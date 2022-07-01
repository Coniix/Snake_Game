using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{   
    public string mainMenuScene;
    public GameObject PauseScreen;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { //escape
            if(!isPaused) {
                isPaused = true;
                PauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else if(isPaused) {
                resumeGame();
            }
        }
    }

    public void resumeGame()
    {
        isPaused = false;
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void returnToMain()
    {
        //isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

}
