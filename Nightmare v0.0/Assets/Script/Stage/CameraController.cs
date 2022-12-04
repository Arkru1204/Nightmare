using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraXpos = 3;
    public float cameraYpos = 2;

    void Start()
    {

    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;  // player의 위치값
        transform.position = new Vector3(playerPos.x + cameraXpos, playerPos.y + cameraYpos, transform.position.z);
        // x,z값은 카메라의 x,z값 y값은 playerPos의 y값으로 가져옴
    }
}
