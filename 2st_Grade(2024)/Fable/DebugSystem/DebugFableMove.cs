using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class DebugFableMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
