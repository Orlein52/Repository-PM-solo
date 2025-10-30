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
    TextMeshProUGUI ammocounter;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ammocounter = GameObject.FindGameObjectWithTag("UI_Ammo").GetComponent<TextMeshProUGUI>();
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
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            deathhighScore.text = "Highscore: " + highScore;
            deathScore.text = "Score: " + score;
            if (score == highScore)
            {

            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            enemySpawn = Random.insideUnitCircle * 6;
            enemy = Random.Range(0, 2);
            spawnLoc = enemySpawn + transform.position;
            waveNumber = maxEnemies - 1;
            wave.text = "Wave " + waveNumber;
            mult.text = waveNumber + "x";
            scoreText.text = "Score: " + score;
            healthBar.fillAmount = (float)player.Health / (float)player.maxHealth;
            if (player.currentWeapon != null)
            {
                weaponUI.SetActive(true);
                ammocounter.text = "Ammo: " + player.currentWeapon.ammo + "/" + player.currentWeapon.maxAmmo;
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
        }
        if (enemyCount == maxEnemies)
        {
            allDead = false;
        }
        if (allDead)
        {
            if (enemy == 0)
            {
                GameObject e = Instantiate(shoot, spawnLoc, transform.rotation);
                enemyCount++;
                waveNumber++;
            }
            if (enemy == 1)
            {
                GameObject e = Instantiate(rush, spawnLoc, transform.rotation);
                enemyCount++;
            }
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
