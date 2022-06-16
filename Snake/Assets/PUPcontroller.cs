using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PUPcontroller : MonoBehaviour
{
    private int pupCounter = 0;
    private int prevPUP = -1;
    private float interval = 10f;
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
        Invoke("RandomisePosition", 5.0f);
    }

    private bool checkPos(float x, float y)
    {
        for(int i = 0; i < snake.segments.Count -1; i++) {
            if(snake.segments[i].position.x == x && snake.segments[i].position.y == y) return false;
        }
        return true;
    }


    private void RandomisePosition() 
    {
        Bounds bounds = this.gridArea.bounds;
    
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
    
        if (checkPos(x, y)) this.transform.position = new Vector3(x, y, 0.0f);
        else RandomisePosition();
        powerUp.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
            RandomisePosition();
            choosePoweUp();
            pickupSoundSource.clip = pickupSound;
            pickupSoundSource.Play();
        }
    }

    private void choosePoweUp() {
        Invoke("RandomisePosition", Random.Range(5, interval));
        int nextPUP = Random.Range(0, pupCounter);
        if (prevPUP == nextPUP) {
            choosePoweUp();
        }
        else {
            prevPUP = nextPUP;
            switch (nextPUP) {
            case 0: //double points
                powerUp.SetActive(false);
                timer.doublePTimer += 15f;
                doublePoints();
                break;
            case 1: //2x speed
                powerUp.SetActive(false);
                timer.doubleSTimer += 15f;
                doubleSpeed();
                break;
            case 2: //locked walls
                powerUp.SetActive(false);
                timer.lockedWallTimer += 15f;
                lockedWalls();
                break;
            case 3: //reversed controls
                powerUp.SetActive(false);
                timer.reversedControllsTimer += 15f;
                ReversedControls();
                break;
        }
        }
    }

    private void doublePoints() {
        pupLabel.text = pupNames[0].ToString();
        snake.scoreIncrement = 2;
    }
    public void resetDoublePoints() {
        snake.scoreIncrement = 1;
        pupLabel.text = "";
    }

    private void doubleSpeed() {
        pupLabel.text = pupNames[1].ToString();
        Time.fixedDeltaTime = 0.035f;
    }
    public void resetDoubleSpeed() {
        Time.fixedDeltaTime = 0.07f;
        pupLabel.text = "";
    }

    private void lockedWalls() {
        pupLabel.text = pupNames[2].ToString();
        wallSpriteN.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteE.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteS.color = new Color(1, 0.92f, 0.016f, 1);
        wallSpriteW.color = new Color(1, 0.92f, 0.016f, 1);
        WallN.tag = "Obstacle";
        WallE.tag = "Obstacle";
        WallS.tag = "Obstacle";
        WallW.tag = "Obstacle";
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
        pupLabel.text = "";
    }

    private void ReversedControls() {
        pupLabel.text = pupNames[3].ToString();
        reversedCon = true;
    }
    public void ResetReversedControls() {
        reversedCon = false;
        pupLabel.text = "";
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
        pupLabel.text = "";
        //label reset
        pupLabel.text = "";
    }
}
