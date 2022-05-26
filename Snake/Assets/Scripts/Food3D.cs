using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food3D : MonoBehaviour
{
    public BoxCollider gridArea;
    public AudioSource eatSoundSource;
    public AudioClip eatSound;
    public Material material;


    private void Start() {
        RandomisePosition();
    }

    private void RandomisePosition() {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        this.transform.position = new Vector3(Mathf.Round(x), 0.5f, Mathf.Round(z));

        GenerateFruit();
    }

    private void GenerateFruit(){
        int fruitType = Random.Range(1,5);

        Debug.Log("Fruit Type = " + fruitType);

        switch (fruitType)
        {
        case 1:
            material.color = Color.blue;
            break;
        case 2:
            material.color = Color.magenta;
            break;
        case 3:
            material.color = Color.red;
            break;
        case 4:
            material.color = Color.green;
            break;
        case 5:
            material.color = Color.yellow;
            break;
        default:
            material.color = Color.black;
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
        eatSoundSource.clip = eatSound;
        eatSoundSource.Play();
        RandomisePosition();
        }
    }
}
