using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mawang : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject AOE;
    [SerializeField] private GameObject lazer;

    MawangStat mawangStat;
    Animator animator;

    public bool isPlayer;
    public bool neverMove;
    public bool isAtk;
    public bool isDie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mawangStat = GetComponent<MawangStat>();
        neverMove = true;
        StartCoroutine(Attack());

    }
    void Update()
    {
        if (FindObjectOfType<DialogueManager>().i >= 70) StartCoroutine(Cooltime(2));
        Flip();
        if (neverMove || isDie) return;
        
    }

    private void Flip()
    {
        if(player.position.x > gameObject.transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
        /*if(!isAtk && !neverMove)
        {
            isAtk = true;

        }*/
    }

    IEnumerator Attack()
    {
        while(true)
        {
            if (isDie) break;
            if (!isAtk && !neverMove)
            {
                print("실행됏흥ㅁㅁ");
                isAtk = true;
                int rd = Random.Range(0, 10);
                print("랜덤값은? " + rd);
                switch (rd)
                {
                    case 1:
                    case 2:
                        Heal();
                        break;
                    case 3:
                    case 4:
                        Ult();
                        break;
                    default:
                        Atk();
                        break;
                }
            }
            yield return null;
        }
        yield return null;
    }
    void Atk()
    {
        int rd = Random.Range(0, 2);
        if(rd == 0)
        {
            fireBall.SetActive(true);
        }
        else
        {
            AOE.SetActive(true);
        }
        animator.SetTrigger("isAtk");
        StartCoroutine(Cooldown(3));
    }
    void Ult()
    {
        lazer.SetActive(true);
        animator.SetTrigger("isFaint");
        StartCoroutine(Cooldown(7));
    }
    void Heal()
    {
        animator.SetTrigger("isHeal");
        if(mawangStat.HP <= mawangStat.currentHP * mawangStat.Enemy_LV - 500)
        mawangStat.HP += 500;
        mawangStat.UpdateHP();
        StartCoroutine(Cooldown(4));
    }

    IEnumerator Cooltime(int time)
    {
        yield return new WaitForSeconds(time);
        neverMove = false;
    }
    IEnumerator Cooldown(int time)
    {
        yield return new WaitForSeconds(time);
        isAtk = false;
        neverMove = false;
    }
}
