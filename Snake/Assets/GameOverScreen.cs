using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public string mainMenuScene;
    public GameObject gameOverS;
    public Snake snake;
    public PUPcontroller pup;
    public PUPtimer timer;


    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        pup = FindObjectOfType<PUPcontroller>();
        timer = FindObjectOfType<PUPtimer>();
    }


    public void isOver()
    {
        gameOverS.SetActive(true);
        Time.timeScale = 0f;
    }

    public void retryGame()
    {
        snake.ResetState();
        pup.ResetPowerUps();
        timer.resetTimers();
        gameOverS.SetActive(false);
        Time.timeScale = 1f;
    }

    public void returnToMain()
    {
        //isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

}
