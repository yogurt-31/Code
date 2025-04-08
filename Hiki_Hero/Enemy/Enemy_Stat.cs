using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Stat : MonoBehaviour
{
    #region 변수타치들
    [SerializeField] Image HP_bar;

    

    public int Enemy_LV;
    public int HP;
    public int ATK;

    public int currentHP = 500;
    int currentATK = 50;

    int fullHP;

    public bool isCrash;
    public bool isDeath;
    public bool canCrash;
    public bool canDotCrash;
    public bool isBiologyE;
    public bool isBiologyQ;
    public bool isE;
    public bool isQ;

    #endregion
    private void Awake()
    {
        HP = currentHP * Enemy_LV;
        ATK = currentATK * Enemy_LV;
        fullHP = HP;
        print("HP" + HP);
        
    }

    private void Update()
    {
        if (isDeath) return;

        if(FindObjectOfType<Player_MOVE>().isAtk && isCrash && !canCrash)
        {
            canCrash = true;
            ChangeHP();
        }
        if (!FindObjectOfType<Player_MOVE>().isAtk)
            canCrash = false;
        if(canDotCrash)
        {
            canDotCrash = false;
            StartCoroutine(DotDeal());
        }
        if(isE && isBiologyE)
        {
            isBiologyE = false;
            StartCoroutine(BirdDeal());
        }
        if (isQ && isBiologyQ)
        {
            isBiologyQ = false;
            HP -= FindObjectOfType<Player_Stat>().ATK * 3;
            HP_bar.fillAmount = (float)HP / fullHP;
        }

        if (HP <= 0)
        {
            isDeath = true;
            if(GetComponent<Enemy_1>())
            {
                GetComponent<Enemy_1>().Death();
            }
            else if(GetComponent<Elisia>())
            {
                GetComponent<Elisia>().Death();
            }
        }
    }

    void ChangeHP()
    {
        print("애앵");
        HP -= FindObjectOfType<Player_Stat>().ATK;
        HP_bar.fillAmount = (float)HP / fullHP;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Normal_Q"))
        {
            canDotCrash = true;
        }
        if (collision.CompareTag("Biology_E") && !isE)
        {
            isE = true;
            isBiologyE = true;
        }
        if(collision.CompareTag("Biology_Q") && !isQ)
        {
            isQ = true;
            isBiologyQ = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        canDotCrash = false;
        if (collision.CompareTag("Player_ATK"))
        {
            print("왜우ㅏㄴ디ㅗ?");
            isCrash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCrash = false;
        isBiologyE = false;
        isBiologyQ = false;
    }
    IEnumerator BirdDeal()
    {
        HP -= FindObjectOfType<Player_Stat>().ATK * 2;
        HP_bar.fillAmount = (float)HP / fullHP;
        yield return new WaitForSeconds(1f);
        isBiologyE = false;
    }
    IEnumerator DotDeal()
    {
        isBiologyE = false;
        print("Q도트딜" + FindObjectOfType<Player_Stat>().ATK * 0.5f);
        HP -= (int)(FindObjectOfType<Player_Stat>().ATK * 0.5f);
        HP_bar.fillAmount = (float)HP / fullHP;

        yield return new WaitForSeconds(0.5f);
        canDotCrash = true;
    }
}
