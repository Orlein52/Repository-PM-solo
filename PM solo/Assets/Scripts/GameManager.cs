using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    GameObject weaponUI;
    Image healthBar;
    TextMeshProUGUI ammocounter;
    TextMeshProUGUI clip;
    GameObject pauseMenu;
    public bool IsPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ammocounter = GameObject.FindGameObjectWithTag("UI_Ammo").GetComponent<TextMeshProUGUI>();
        healthBar = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<Image>();
        clip = GameObject.FindGameObjectWithTag("UI_Clip").GetComponent<TextMeshProUGUI>();
        weaponUI = GameObject.FindGameObjectWithTag("Weapon_UI");
        pauseMenu = GameObject.FindGameObjectWithTag("UI_Pause");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            healthBar.fillAmount = (float)player.Health / (float)player.MaxHealth;
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

    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
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
