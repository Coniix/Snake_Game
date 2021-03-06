using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class Snake : MonoBehaviour
{
    public TMP_Text scoreCounter;
    private Vector2 _direction = Vector2.right;
    public List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initSize = 3;
    public string MainMenu;
    public int currScore = 0;
    public int scoreIncrement = 1;
    private bool stepped = false;
    public int speed = 1;
    public GameOverScreen gameOver;
    public PUPcontroller pup;

    private void Start()
    {
        gameOver = FindObjectOfType<GameOverScreen>();
        pup = FindObjectOfType<PUPcontroller>();
        ResetState();
    } 

    private void Update() 
    {
        GetInput();
    }
    
    private void GetInput() {
        if(!stepped) {
            if (Input.GetKeyDown(KeyCode.W)) { //up - w
                if (_direction == Vector2.down) {}
                else if (pup.reversedCon && _direction != Vector2.up) {
                    _direction = Vector2.down;
                }
                else {
                    _direction = Vector2.up;
                    stepped = true;
                }
            } 
            else if (Input.GetKeyDown(KeyCode.S)) { //down - s
                if (_direction == Vector2.up) {}
                else if (pup.reversedCon && _direction != Vector2.down) {
                    _direction = Vector2.up;
                }
                else {
                    _direction = Vector2.down;
                    stepped = true;
                }
            } 
            else if (Input.GetKeyDown(KeyCode.A)) { //left - a
                if (_direction == Vector2.right) {}
                else if (pup.reversedCon && _direction != Vector2.left) {
                    _direction = Vector2.right;
                }
                else {
                    _direction = Vector2.left;
                    stepped = true;
                }
            } 
            else if (Input.GetKeyDown(KeyCode.D)) { //right - d
                if (_direction == Vector2.left) {}
                else if (pup.reversedCon && _direction != Vector2.right) {
                    _direction = Vector2.left;
                }
                else {
                    _direction = Vector2.right;
                    stepped = true;
                }
            } 
            else if (Input.GetKeyDown(KeyCode.K)) { //kill - k
                ResetState();
            }
        }
    }

    private void FixedUpdate() 
    {   
        stepped = false;
        for(int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {       
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        currScore += scoreIncrement;
        scoreCounter.text = currScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Food") { Grow(); }
        else if (other.tag == "Obstacle") { gameOver.isOver(); }
        else if (other.tag == "Wall") { wrapAround(); }
    }

    private void wrapAround()
    {
        if (this.transform.position.x == 24) { //right to left
            this.transform.position = new Vector3(
                this.transform.position.x - 47,
                this.transform.position.y,
                0.0f
            );
        } else if (this.transform.position.x == -24) { //left to right
            this.transform.position = new Vector3(
                this.transform.position.x + 47,
                this.transform.position.y,
                0.0f
            );
        } else if (this.transform.position.y == 10) { //up to down
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y - 23,
                0.0f
            );
        } else if (this.transform.position.y == -14) { //down to up
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + 23,
                0.0f
            );
        }
    }

    public void ResetState()
    {
        for(int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for(int i = 1; i < this.initSize; i++) {
            segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
        this.currScore = 0;
        scoreCounter.text = currScore.ToString();
        _direction = Vector2.right;
    }
}
