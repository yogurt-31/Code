using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasya : MonoBehaviour
{
    [SerializeField] private Transform warp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject canvas;
    void Update()
    {
        if(FindObjectOfType<DialogueManager>().i >= 25)
        {
            if(transform.position.y <= 11f)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position = warp.position;
                canvas.SetActive(true);
            }
        }
    }
}
