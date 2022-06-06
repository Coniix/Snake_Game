using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUPcontroller : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Snake snake;
    
    // Start is called before the first frame update
    void Start()
    {
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        RandomisePosition();
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
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {RandomisePosition();}
    }
}
