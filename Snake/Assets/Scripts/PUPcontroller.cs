using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PUPcontroller : MonoBehaviour
{
    private int pupCounter = 0;
    private int prevPUP = -1;
    public float interval = 20f;
    public bool reversedCon = false;
    public BoxCollider2D gridArea;
    public Snake snake;
    public List<string> pupNames = new List<string>();
    public TMP_Text pupLabel;
    public PUPtimer timer;
    public GameObject powerUp;
    public SpriteRenderer wallSpriteN;
    public SpriteRenderer wallSpriteE;
    public SpriteRenderer wallSpriteS;
    public SpriteRenderer wallSpriteW;
    public GameObject WallN;
    public GameObject WallE;
    public GameObject WallS;
    public GameObject WallW;
    public AudioClip pickupSound;
    public AudioSource pickupSoundSource;

    public GameObject DoubleSpeedIcon;
    public GameObject DoublePointsIcon;
    public GameObject LockedWallsIcon;
    public GameObject ReversedControllsIcon;
    public GameObject countDownObject;


    // Start is called before the first frame update
    void Start()
    {
        pupNames.Add("Double Points");
        pupNames.Add("2x Speed");
        pupNames.Add("Locked Walls");
        pupNames.Add("Reversed Controlls");

        pupCounter = pupNames.Count;

        wallSpriteN = WallN.GetComponent<SpriteRenderer>();
        wallSpriteE = WallE.GetComponent<SpriteRenderer>();
        wallSpriteS = WallS.GetComponent<SpriteRenderer>();
        wallSpriteW = WallW.GetComponent<SpriteRenderer>();

        timer = FindObjectOfType<PUPtimer>();
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        Invoke("choosePowerUp", 5.0f);
        countDownObject.SetActive(true);
        StartCoroutine(countdownToPowerUp (countDownObject, 5.0f));
    }

    private void choosePowerUp() {
        countDownObject.GetComponent<Image>().fillAmount = 1f;
        float timeToNextPowerUp = Random.Range(8, interval);

        int nextPUP = Random.Range(0, pupCounter);
        if (prevPUP == nextPUP) {
            choosePowerUp();
        }
        else {
            StartCoroutine(countdownToPowerUp (countDownObject, timeToNextPowerUp - 0.1f));
            Invoke("choosePowerUp", timeToNextPowerUp);
            pickupSoundSource.clip = pickupSound;
            pickupSoundSource.Play();

            prevPUP = nextPUP;
            switch (nextPUP) {
            case 0: //double points
                timer.doublePTimer += 5f;
                doublePoints();
                DoublePointsIcon.GetComponent<Image>().fillAmount = 1f;
                StartCoroutine(countdownToPowerUp (DoublePointsIcon, timer.doublePTimer - 0.1f));
                break;
            case 1: //2x speed
                timer.doubleSTimer += 5f;
                doubleSpeed();
                DoubleSpeedIcon.GetComponent<Image>().fillAmount = 1f;
                StartCoroutine(countdownToPowerUp (DoubleSpeedIcon, timer.doubleSTimer - 0.1f));
                break;
            case 2: //locked walls
                timer.lockedWallTimer += 5f;
                lockedWalls();
                LockedWallsIcon.GetComponent<Image>().fillAmount = 1f;
                StartCoroutine(countdownToPowerUp (LockedWallsIcon, timer.lockedWallTimer - 0.1f));
                break;
            case 3: //reversed controls
                timer.reversedControllsTimer += 5f;
                ReversedControls();
                ReversedControllsIcon.GetComponent<Image>().fillAmount = 1f;
                StartCoroutine(countdownToPowerUp (ReversedControllsIcon, timer.reversedControllsTimer - 0.1f));
                break;
        }
        }
    }

    IEnumerator countdownToPowerUp(GameObject icon, float timeToNextPowerUp){
        float elapsedTime = 0f;
                Debug.Log("........." + timeToNextPowerUp);

        while(elapsedTime < timeToNextPowerUp){
            elapsedTime += Time.deltaTime;
            icon.GetComponent<Image>().fillAmount = icon.GetComponent<Image>().fillAmount - Time.deltaTime/timeToNextPowerUp;
            yield return null;
        }
    }

    private void doublePoints() {
        //pupLabel.text = pupNames[0].ToString();
        snake.scoreIncrement = 2;
        DoublePointsIcon.SetActive(true);
    }
    public void resetDoublePoints() {
        snake.scoreIncrement = 1;
        //pupLabel.text = "";
        DoublePointsIcon.SetActive(false);
    }

    private void doubleSpeed() {
        //pupLabel.text = pupNames[1].ToString();
        Time.fixedDeltaTime = 0.035f;
        DoubleSpeedIcon.SetActive(true);
    }
    public void resetDoubleSpeed() {
        Time.fixedDeltaTime = 0.07f;
        //pupLabel.text = "";
        DoubleSpeedIcon.SetActive(false);
    }

    private void lockedWalls() {
        //pupLabel.text = pupNames[2].ToString();
        wallSpriteN.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteE.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteS.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteW.color = new Color(1, 0.92f, 0.016f, 1);
        /*WallN.tag = "Obstacle";
        WallE.tag = "Obstacle";
        WallS.tag = "Obstacle";
        WallW.tag = "Obstacle";*/
        LockedWallsIcon.SetActive(true);
    }
    public void resetLockedWalls() {
        wallSpriteN.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteE.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteS.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteW.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        WallN.tag = "Wall";
        WallE.tag = "Wall";
        WallS.tag = "Wall";
        WallW.tag = "Wall";
        //pupLabel.text = "";
        LockedWallsIcon.SetActive(false);
    }

    private void ReversedControls() {
        //pupLabel.text = pupNames[3].ToString();
        reversedCon = true;
        ReversedControllsIcon.SetActive(true);
    }
    public void ResetReversedControls() {
        reversedCon = false;
        //pupLabel.text = "";
        ReversedControllsIcon.SetActive(false);
    }

    public void ResetPowerUps() {
        //reset double points
        snake.scoreIncrement = 1;
        //reset speed
        Time.fixedDeltaTime = 0.07f;
        //wall reset
        wallSpriteN.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteE.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteS.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        wallSpriteW.color = new Color(0.50980392f, 0.44313725f, 0.44313725f, 1);
        WallN.tag = "Wall";
        WallE.tag = "Wall";
        WallS.tag = "Wall";
        WallW.tag = "Wall";
        //controls reset
        reversedCon = false;
        //pupLabel.text = "";
        //label reset
        //pupLabel.text = "";
    }
}
