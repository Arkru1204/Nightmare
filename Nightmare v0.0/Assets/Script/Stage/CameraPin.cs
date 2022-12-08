using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPin : MonoBehaviour
{
    public CameraController cameraController;
    public CameraControllerTmp cameraControllerTmp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraController.enabled = false;
            cameraControllerTmp.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraController.enabled = true;
            cameraControllerTmp.enabled = false;
        }
    }
}
