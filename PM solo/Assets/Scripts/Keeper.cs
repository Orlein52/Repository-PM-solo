using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Keeper : MonoBehaviour
{
    GameManager gameManager;
    public int highscore = 0;
    public int score = 0;
    TextMeshProUGUI deathScore;
    TextMeshProUGUI deathhighScore;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        if (score >= highscore)
        {
            highscore = score;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            score = gameManager.score;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            deathScore = GameObject.FindGameObjectWithTag("Death_Score").GetComponent<TextMeshProUGUI>();
            deathhighScore = GameObject.FindGameObjectWithTag("Highscore").GetComponent<TextMeshProUGUI>();
            deathhighScore.text = "Highscore: " + highscore;
            deathScore.text = "Score: " + score;
            if (score == highscore)
            {
                deathhighScore.color = Color.yellow;
            }
        }


    }
}
