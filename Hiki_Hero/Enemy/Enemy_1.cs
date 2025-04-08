using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_1 : MonoBehaviour, Enemy_Option
{
    Rigidbody2D rigid;

    GameObject enemyFx;
    public bool isPlayer;
    public int flip;
    public bool isAtk;
    public bool isAtkCoolTime;

    [SerializeField] Transform target;
    [SerializeField] GameObject particle;

    Vector3 vec;

    public float moveSpeed;
    private void Awake()
    {
        target = FindObjectOfType<Player_Ray>().transform;
        rigid = GetComponent<Rigidbody2D>();
        enemyFx = GetComponentInChildren<Enemy_FX>().gameObject;
        enemyFx.SetActive(false);

        
        StartCoroutine(Move());
        print("응애 플레이어 위치: " + target.position);
    }
    void FixedUpdate()
    {
        if (GetComponent<Enemy_Stat>().isDeath) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 12f)
        {
            What();
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);
        }
    }

    void What()
    {
        Vector3 vector = new Vector3(1.2f * flip, 0);
        if (!isPlayer)
        {
            rigid.velocity = vec * moveSpeed;
        }
        else
        {
            if (target.position.x < transform.position.x)
                flip = 1;
            else if (target.position.x > transform.position.x)
                flip = -1;

            transform.position = Vector3.MoveTowards(transform.position, target.position + vector,
                                  Time.deltaTime * moveSpeed * 1.5f);

            rigid.velocity = Vector3.zero;
        }

        if (target.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (isAtk && !isAtkCoolTime)
            StartCoroutine(Atk());
    }
    public IEnumerator Move()
    {
        while (true)
        {
            if (!isPlayer)
            {
                vec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                vec = Vector3.zero;
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            }
            yield return null;
        }
    }
    public void Death()
    {
        GetComponentInParent<Spawner>().enemyCount += 1;
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public IEnumerator Atk()
    {
        isAtkCoolTime = true;
        enemyFx.SetActive(true);
        FindObjectOfType<Player_Stat>().HP -= 50;
        yield return new WaitForSeconds(3f);
        isAtkCoolTime = false;
    }
}
