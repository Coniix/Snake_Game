using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    [Range(2, 8)]
    public float camDistance = 4f;
    [Range(1f, 2.5f)]
    public float camHeight = 2f;
    [Range(5, 15)]
    public float camXSensitivity = 12.5f;
    [Range(5, 15)]
    public float camYSensitivity = 10f;

    GameObject player;
    Vector3 currentRotation;
    Vector3 playerPos;
    public float pitch;
    public float yaw;
    GameObject food;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        food = GameObject.Find("Food");
    }

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.LookAt(food.transform.position, Vector3.up);
        }
        else{
            playerPos = player.transform.position;

            //Camera Rotation and Clamping
            yaw += Input.GetAxis("LookX") * camXSensitivity * Time.deltaTime * 10;
            pitch += -Input.GetAxis("LookY") * camYSensitivity * Time.deltaTime * 10;
            pitch = Mathf.Clamp(pitch, -40, 50);

            float pitchToHeightRatio;
        /* if(Input.GetKeyDown(KeyCode.A)){
                yaw -= 90f;
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                yaw += 90f;
            }*/
            pitchToHeightRatio = 1f + ((pitch + 40) / 60);

            camHeight = pitchToHeightRatio;

            currentRotation = new Vector3(pitch, yaw);
            transform.eulerAngles = currentRotation;

            //Control Camera distance from Player and collision detection
            Ray ray = new Ray(new Vector3(playerPos.x, playerPos.y + camHeight, playerPos.z), -transform.forward);
            Debug.DrawRay(new Vector3(player.transform.position.x, player.transform.position.y + camHeight, player.transform.position.z), -transform.forward * camDistance, Color.yellow);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 15f, 3))
            {
                if (hit.distance < camDistance && hit.distance > 2f)
                    transform.position = ray.GetPoint(hit.distance);
                else if (hit.distance >= camDistance)
                    transform.position = ray.GetPoint(camDistance);
                else
                    transform.position = ray.GetPoint(hit.distance);
            }
            else
            {
                transform.position = ray.GetPoint(camDistance);
            }
        }
    }
}
