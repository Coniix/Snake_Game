using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public AudioSource eatSoundSource;
    public AudioClip eatSound;
    public Snake snake;
    


    private void Start() {
        snake = GameObject.Find("Snake").GetComponent<Snake>();
        RandomisePosition();
    }

    private void trans(float x, float y) {
        this.transform.position = new Vector3(x, y, 0.0f);
        Debug.Log("transformed");
    }

    private void RandomisePosition() 
    {
        Bounds bounds = this.gridArea.bounds;
    
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
        Debug.Log(x + " " + y);

        for(int i = 0; i < snake.segments.Count -1; i++) {
            //Debug.Log(snake.segments[i].position);
            //Debug.Log("x" + x);
            //Debug.Log("y" + y);
            if(snake.segments[i].position.x == x && snake.segments[i].position.y == y) {
                Debug.Log("Error");
                RandomisePosition();
            }
        }
        //trans(x, y);
        this.transform.position = new Vector3(x, y, 0.0f);
        Debug.Log("Repositioned");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") {
        eatSoundSource.clip = eatSound;
        eatSoundSource.Play();
        RandomisePosition();
        }
    }
}
