using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amon : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        transform.position = new Vector3(target.position.x - 15f, target.position.y);
    }
}
