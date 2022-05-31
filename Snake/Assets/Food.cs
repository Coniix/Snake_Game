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


    private void RandomisePosition() 
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        for(int i = 0; i < snake.segments.Count -1; i++) {
            if(snake.segments[i].position.x == x && snake.segments[i].position.y == y) RandomisePosition();
        }

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
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
