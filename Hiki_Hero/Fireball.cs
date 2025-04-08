using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject fireball_Collider;
    [SerializeField] private float moveSpeed;

    Mawang mawang;

    float y;
    Vector3 vec;
    bool canAtk;

    private void Awake()
    {
        mawang = GetComponent<Mawang>();
    }

    void OnEnable()
    {
        y = target.position.y;
        transform.position = new Vector3(target.position.x+ 0.5f, y + 13f);
        vec = Vector3.down;
        StartCoroutine(Atk());
    }
    void Update()
    {
        transform.position += vec * moveSpeed * Time.deltaTime;
        if (y >= transform.position.y)
        {
            vec = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    public IEnumerator Cooltime(int time)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        mawang.isAtk = false;
        mawang.neverMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canAtk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canAtk = false;
    }

    IEnumerator Atk()
    {
        while(true)
        {
            if (canAtk && FindObjectOfType<Mawang>().isAtk)
            {
                canAtk = false;
                FindObjectOfType<Player_Stat>().HP -= FindObjectOfType<MawangStat>().ATK;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
