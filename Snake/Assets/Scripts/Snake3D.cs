using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class Snake3D : MonoBehaviour
{
   public TMP_Text scoreCounter;
    public Vector2 _direction = Vector2.up;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initSize = 3;
    public int currScore = 0;

    private void Start()
    {
        ResetState();
    } 

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.A)) { //left - a
            if (_direction == Vector2.right) {
               _direction = Vector2.up;
            }
            else if (_direction == Vector2.up){
               _direction = Vector2.left;
            }
            else if (_direction == Vector2.left){
               _direction = Vector2.down;
            }
            else if (_direction == Vector2.down){
               _direction = Vector2.right;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.D)) { //right - d
           if (_direction == Vector2.right) {
               _direction = Vector2.down;
            }
            else if (_direction == Vector2.up){
               _direction = Vector2.right;
            }
            else if (_direction == Vector2.left){
               _direction = Vector2.up;
            }
            else if (_direction == Vector2.down){
               _direction = Vector2.left;
            }
        } else if (Input.GetKeyDown(KeyCode.K)) { //kill - k
            ResetState();
        }
    }

    private void FixedUpdate() 
    {
        for(int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            0.5f,
            Mathf.Round(this.transform.position.z) + _direction.y
        );
    }

    private void Grow(Color color)
    {
        int segmentsToAdd = 0;

        Debug.Log("Colour is " + color.ToString());

        if(color == Color.green){
            segmentsToAdd = 1;
        }
        else if(color == Color.yellow){
            segmentsToAdd = 2;
        }
        else if(color == Color.red){
            segmentsToAdd = 3;
        }
        else if(color == Color.blue){
            segmentsToAdd = 4;
        }
        else if(color == Color.magenta){
            segmentsToAdd = 5;
        }
        else if(color == Color.black){
            segmentsToAdd = 6;
        }
        Debug.Log("Segments to add = " + segmentsToAdd);

        for(int i = 0; i <= segmentsToAdd; i++){
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;
            _segments.Add(segment);
            currScore++;
            scoreCounter.text = currScore.ToString();
            //Debug.Log(currScore);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Food") { Grow(other.GetComponent<MeshRenderer>().material.color); }
        else if (other.tag == "Obstacle") { ResetState(); }
        else if (other.tag == "Wall") { wrapAround(); }
    }

    private void wrapAround()
    {
        if (this.transform.position.x > 23) { //right to left
            this.transform.position = new Vector3(
                this.transform.position.x - 48,
                0.5f,
                this.transform.position.z
            );
        } else if (this.transform.position.x < -23) { //left to right
            this.transform.position = new Vector3(
                this.transform.position.x + 48,
                0.5f,
                this.transform.position.z
            );
        } else if (this.transform.position.z > 23) { //up to down
            this.transform.position = new Vector3(
                this.transform.position.x,
                0.5f,
                this.transform.position.z - 48
            );
        } else if (this.transform.position.z < -23) { //down to up
            this.transform.position = new Vector3(
                this.transform.position.x,
                0.5f,
                this.transform.position.z + 48
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

        this.transform.position = new Vector3(0f, 0.5f, 1f);
        this.currScore = 0;
        scoreCounter.text = currScore.ToString();
    }
}
