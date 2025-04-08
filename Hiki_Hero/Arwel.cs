using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arwel : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 vec;
    private void OnEnable()
    {
        vec = new Vector3(target.position.x + 1.5f, target.position.y);
    }
    void FixedUpdate()
    {
        transform.position = vec;
    }
}
