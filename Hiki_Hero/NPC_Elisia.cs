using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Elisia_Type
{
    그냥등장,
    달리면서등장
}

public class NPC_Elisia : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Elisia_Type 뭐로할겨;
    public float moveSpeed;

    private void Awake()
    {
        transform.position = new Vector3(target.position.x - 15f, target.position.y);
    }
    void Update()
    {
        switch (뭐로할겨)
        {
            case Elisia_Type.그냥등장:
                transform.position = new Vector3(target.position.x - 3f, target.position.y);
                break;
            case Elisia_Type.달리면서등장:
                GetComponent<Animator>().SetInteger("Move", 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                if (target.position.x - 3f <= transform.position.x)
                {
                    moveSpeed = 0;
                    GetComponent<Animator>().SetInteger("Move", 0);
                } 
                break;
        }
        
    }

}
