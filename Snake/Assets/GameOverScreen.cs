using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{   
    public string mainMenuScene;
    public GameObject gameOverS;
    //public bool isPaused;
    public Snake snake;


    private void Start()
    {
        snake = FindObjectOfType<Snake>();
    }

    public void isOver()
    {
        gameOverS.SetActive(true);
        Time.timeScale = 0f;
    }

    public void retryGame()
    {
        //isPaused = false;
        snake.ResetState();
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
