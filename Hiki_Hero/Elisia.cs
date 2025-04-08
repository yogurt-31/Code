using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum story_Spawn
{
    Yes,
    No
}
public class Elisia : MonoBehaviour, Enemy_Option
{
    public bool isAtk;
    public bool isAtkCoolTime;
    public bool isPlayer;
    public bool neverMove;

    public story_Spawn storySpawn;

    Animator animator;
    Enemy_Stat enemy_Stat;

    public float moveSpeed;
    public float defaultSpeed;
    public float flip;

    [SerializeField] private GameObject dialogue_Trigger;
    [SerializeField] private Transform target;

    private void Awake()
    {
        defaultSpeed = moveSpeed;
        enemy_Stat = GetComponent<Enemy_Stat>();
        animator = GetComponent<Animator>();
        neverMove = true;
        StartCoroutine(coolTime());
    }
    public IEnumerator Atk()
    {
        moveSpeed = 0;
        isAtkCoolTime = true;
        animator.SetTrigger("Atk");
        FindObjectOfType<Player_Stat>().HP -= 50;
        yield return new WaitForSeconds(1f);
        moveSpeed = defaultSpeed;
        yield return new WaitForSeconds(1f);
        isAtkCoolTime = false;
    }

    IEnumerator coolTime()
    {
        yield return new WaitForSeconds(2f);
        neverMove = false;
    }

    public void Death()
    {
        animator.SetBool("Die", true);
        StartCoroutine(Move());
        Destroy(gameObject, 1.2f);
    }

    public IEnumerator Move() // 함수 이름이 Move인데 정작 움직이지는 않는
    {
        FindObjectOfType<Player_Stat>().LevelUp();
        yield return new WaitForSeconds(1.1f); 
        FindObjectOfType<DialogueManager>().nextDialogue = true;
        dialogue_Trigger.SetActive(true);
    }
    void Update()
    {
        if (animator.GetBool("Die") || neverMove) return;
        if (target.position.x < transform.position.x)
            flip = 1;
        else if (target.position.x > transform.position.x)
            flip = -1;
        Vector3 vector = new Vector3(1.2f * flip, 0);
        transform.localScale = new Vector3(flip, 1);
        transform.position = Vector3.MoveTowards(transform.position, target.position + vector,
                                  Time.deltaTime * moveSpeed * 1.5f);
        if (target.position.x + vector.x - transform.position.x > -1 && target.position.x + vector.x - transform.position.x < 1)
        {
            if(target.position.y + vector.y - transform.position.y > -0.1 && target.position.y + vector.y - transform.position.y < 0.1)
            {
                animator.SetInteger("Move", 0);
            }
        }
        else
        {
            animator.SetInteger("Move", 1);
        }

        if(isPlayer && !isAtkCoolTime && !enemy_Stat.isDeath)
        {
            StartCoroutine(Atk());
        }
    }
}
