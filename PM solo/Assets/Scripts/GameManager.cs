using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Properties;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    GameObject weaponUI;
    Image healthBar;
    Keeper keeper;
    TextMeshProUGUI clip;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI wave;
    TextMeshProUGUI mult;
    TextMeshProUGUI deathScore;
    TextMeshProUGUI deathhighScore;
    GameObject pauseMenu;
    public bool IsPaused = false;
    public GameObject shoot;
    public GameObject rush;
    Vector3 enemySpawn;
    public bool allDead;
    public int enemyCount;
    public int maxEnemies;
    public int enemy;
    public int score;
    public int waveNumber = 1;
    Vector3 spawnLoc;
    public int highScore;
    public bool bestRun;
    public Node node;
    public int enNum;
    Node test;
    void Start()
    {
        keeper = GameObject.FindGameObjectWithTag("Keeper").GetComponent<Keeper>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            healthBar = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<Image>();
            clip = GameObject.FindGameObjectWithTag("UI_Clip").GetComponent<TextMeshProUGUI>();
            weaponUI = GameObject.FindGameObjectWithTag("Weapon_UI");
            pauseMenu = GameObject.FindGameObjectWithTag("UI_Pause");
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            scoreText = GameObject.FindGameObjectWithTag("UI_Score").GetComponent<TextMeshProUGUI>();
            wave = GameObject.FindGameObjectWithTag("UI_Wave").GetComponent<TextMeshProUGUI>();
            mult = GameObject.FindGameObjectWithTag("UI_Mult").GetComponent<TextMeshProUGUI>();
            pauseMenu.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            deathScore = GameObject.FindGameObjectWithTag("Death_Score").GetComponent<TextMeshProUGUI>();
            deathhighScore = GameObject.FindGameObjectWithTag("Highscore").GetComponent<TextMeshProUGUI>();
        }
        Time.timeScale = 1;
        //Waves (cumulative)
        //1
        node = new Node(2, 0);
        node.Add(1);
        //2
        node.Add(2);
        //3
        node.Add(2);
        //4
        node.Add(1);
        //5
        node.Add(1);
        //AddAt & RemoveAt/Remove tests
        test = new Node(1, 0);
        test.Add(1);
        test.Add(0);
        test.Add(1);
        test.Add(0);
        test.AddAt(2,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            enemySpawn = Random.insideUnitCircle * 2;
            if (waveNumber <= 5)
            {
                enemy = node.Get(enNum);
            }
            if (waveNumber > 5)
            {
                enemy = Random.Range(1, 2);
            }
            spawnLoc = enemySpawn + transform.position;
            waveNumber = maxEnemies - 1;
            wave.text = "Wave " + waveNumber;
            mult.text = waveNumber + "x";
            scoreText.text = "Score: " + score;
            healthBar.fillAmount = (float)player.Health / (float)player.maxHealth;
            if (player.currentWeapon != null)
            {
                weaponUI.SetActive(true);
                clip.text = "Clip = " + player.currentWeapon.clip + "/" + player.currentWeapon.clipSize;
            }
            else
            {
                weaponUI.SetActive(false);
            }
            if (score > highScore)
            {
                scoreText.color = Color.yellow;
            }
            if (enemyCount == 0)
            {
                allDead = true;
                enNum = 0;
            }
            if (enemyCount == maxEnemies)
            {
                allDead = false;
            }
            if (allDead)
            {
                if (enemy == 2)
                {
                    GameObject e = Instantiate(rush, spawnLoc, transform.rotation);
                    enemyCount++;
                }
                if (enemy == 1)
                {
                    GameObject e = Instantiate(shoot, spawnLoc, transform.rotation);
                    enemyCount++;
                }
                enNum++;
            }
            Debug.Log(test.Get(2));
        }
    }
    public void Pause()
    {
        if (!IsPaused)
        {
            pauseMenu.SetActive(true);
            IsPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Resume();
        }
    }
    public void Resume()
    {
        if (IsPaused)
        {
            IsPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void Respawn()
    {
        SceneManager.LoadScene(1);
        score = 0;
        maxEnemies = 2;
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    public void Death()
    {
        score += 10 * waveNumber;
        enemyCount--;
        if (enemyCount == 0)
        {
            maxEnemies++;
            allDead = true;
        }
    }
    public void PlayerDeath()
    {
        SceneManager.LoadScene(2);
        if (score > highScore)
        {
            bestRun = true;
            highScore = score;
        }
    }
    public void MainMenu()
    {
        LoadLevel(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
