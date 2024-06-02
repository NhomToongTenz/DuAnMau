using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameSession : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    public int playerlives = 3;
    public Text playerlivesText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: "+score.ToString();
        playerlivesText.text = "LIVES: "+playerlives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        int number = FindObjectsOfType<GameSession>().Length;
        if (number > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    //Khi player chet
    public void PlayerDeath()
    {
        if (playerlives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void TakeLife()
    {
        playerlives--;

        //lay index cua scene hien tai
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        //load lai scene hien tai
        SceneManager.LoadScene(currentsceneindex);

        playerlivesText.text = "LIVES: " + playerlives.ToString();

    }

    public void ResetGameSession()
    {

        SceneManager.LoadScene(1);
        Destroy(gameObject); //destroy GameSession luon
        Time.timeScale = 1;
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score: " + score.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
