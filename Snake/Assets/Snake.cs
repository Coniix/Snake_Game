using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class Snake : MonoBehaviour
{
    public TMP_Text scoreCounter;
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initSize = 3;
    public string MainMenu;
    public int currScore = 0;

    private void Start()
    {
        ResetState();
    } 

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.W)) { //up - w
            if (_direction == Vector2.down) {}
            else {_direction = Vector2.up;}
        } 
        else if (Input.GetKeyDown(KeyCode.S)) { //down - s
            if (_direction == Vector2.up) {}
            else {_direction = Vector2.down;}
        } 
        else if (Input.GetKeyDown(KeyCode.A)) { //left - a
           if (_direction == Vector2.right) {}
           else {_direction = Vector2.left;}
        } 
        else if (Input.GetKeyDown(KeyCode.D)) { //right - d
           if (_direction == Vector2.left) {}
           else {_direction = Vector2.right;}
        }
    }

    private void FixedUpdate() 
    {
        for(int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
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
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        currScore++;
        scoreCounter.text = currScore.ToString();
        Debug.Log(currScore);

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Food") { Grow(); }
        else if (other.tag == "Obstacle") { ResetState(); }
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
        } else if (this.transform.position.y == 12) { //up to down
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y - 23,
                0.0f
            );
        } else if (this.transform.position.y == -12) { //down to up
            this.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + 23,
                0.0f
            );
        }
    }

    private void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 1; i < this.initSize; i++) {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }
}
