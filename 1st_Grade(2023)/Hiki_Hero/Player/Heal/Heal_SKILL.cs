using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_SKILL : MonoBehaviour
{
    [SerializeField] private GameObject E_effect;
    [SerializeField] private GameObject Q_effect;

    bool isESkill;
    bool isQSkill;

    Player_MOVE playerMove;
    Player_UI player_UI;

    public Player_Stat playerStat;

    private void Awake()
    {
        playerMove = GetComponentInParent<Player_MOVE>();
        player_UI = FindObjectOfType<Player_UI>();
        playerStat = GetComponentInParent<Player_Stat>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isESkill)
        {
            isESkill = true;
            StartCoroutine(E());
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isQSkill)
        {
            isQSkill = true;
            StartCoroutine(Q());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        isESkill = false;
        isQSkill = false;
    }
    IEnumerator Heal()
    {
        for (int i = 0; i < 8; i++)
        {
            if (playerStat.HP >= playerStat.currentHP * playerStat.Player_LV) break;
            playerStat.HP += playerStat.Player_LV * 50;
            FindObjectOfType<StatManager>().SendStat(playerStat.HP, playerStat.ATK);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
    IEnumerator E()
    {
        StartCoroutine(player_UI.E_CoolTime(15));
        E_effect.SetActive(true);
        StartCoroutine(Heal());
        yield return new WaitForSeconds(8f);
        E_effect.SetActive(false);
        yield return new WaitForSeconds(7f);
        isESkill = false;
    }

    IEnumerator Q()
    {
        StartCoroutine(player_UI.Q_CoolTime(20));
        Q_effect.SetActive(true);
        GetComponentInParent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(10f);
        GetComponentInParent<BoxCollider2D>().enabled = true;
        Q_effect.SetActive(false);
        yield return new WaitForSeconds(10f);
        isQSkill = false;   
    }

    public void IsAtk() // 애니메이션 이벤트용 함수.
    {
        playerMove.isAtk = false;
        GetComponentInChildren<Animator>().SetBool("isAtk", playerMove.isAtk);
        playerMove.speed = playerMove.defaultSpeed;
    }
    private void OnEnable()
    {
        playerMove.speed = playerMove.defaultSpeed;
        playerMove.isAtk = false;
        player_UI.Q_Exist();
    }
}
