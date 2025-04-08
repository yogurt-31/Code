using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public float cameraMoveSpeed;

    Vector3 cameraPosition = new Vector3(0, -1, -10);
    Transform playerTransform;

    private void Start()
    {
        try
        {
            playerTransform = FindObjectOfType<Player_MOVE>().transform;
        }
        catch
        {
            print("플레이어가 없자나..");
        }
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition,
                                  Time.deltaTime * cameraMoveSpeed);
    }

    public void ChangeTarget()
    {
        try
        {
            playerTransform = FindObjectOfType<Player_MOVE>().transform;
        }
        catch
        {
            print("플레이어가 없자나..");
        }
    }

}
