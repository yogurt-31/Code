using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Raycast : MonoBehaviour
{
    public Vector2 size;
    public LayerMask whatIsLayer;

    void Update()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, size, 0, whatIsLayer);
        if(hit != null)
        {
            print(hit);
            if (hit.CompareTag("Player"))
            {
                if(GetComponentInParent<Enemy_1>() != null)
                {
                    GetComponentInParent<Enemy_1>().isPlayer = true;
                }
                else if(GetComponent<Elisia>() != null)
                {
                    GetComponentInParent<Elisia>().isPlayer = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
