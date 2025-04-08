using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Player_Movement : MonoBehaviour
{
    public float speed;
    public bool isDoor;

    Animator animator;
    Rigidbody2D rigid;
    DialogueManager dialogue;

    Vector3 dirVec;

    private void Awake()
    {
        dialogue = FindObjectOfType<DialogueManager>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (dialogue.isTalk)
        {
            rigid.velocity = Vector3.zero;
            animator.SetInteger("isRun", 0);
            return;
        }
        Move();

        if (isDoor && Input.GetKeyDown(KeyCode.F))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scarlet");
    }

    void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(x, y).normalized * speed;

        animator.SetInteger("isRun", x == 0 ? (int)y : (int)x);

        if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("끼엑");
        if(collision.CompareTag("ArwelDoor"))
        {
            isDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isDoor = false;
    }
}
