using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food3D : MonoBehaviour
{
    public BoxCollider gridArea;
    public AudioSource eatSoundSource;
    public AudioClip eatSound;


    private void Start() {RandomisePosition();}

    private void RandomisePosition() {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        this.transform.position = new Vector3(Mathf.Round(x), 0.5f, Mathf.Round(z));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
        eatSoundSource.clip = eatSound;
        eatSoundSource.Play();
        RandomisePosition();
        }
    }
}
