using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_CameraMoving : MonoBehaviour
{
    public float cameraMoveSpeed;

    Vector3 cameraPosition = new Vector3(0, -1, -10);
    Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<Intro_Player_Movement>().transform;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition,
                                  Time.deltaTime * cameraMoveSpeed);
    }
}
