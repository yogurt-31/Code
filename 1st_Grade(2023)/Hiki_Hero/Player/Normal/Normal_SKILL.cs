using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Normal_SKILL : MonoBehaviour
{
    #region 변수타치들
    [SerializeField] private GameObject E_effect; // E이펙트
    [SerializeField] private GameObject Q_effect; // Q이펙트
    [SerializeField] private GameObject 피해구역; // 도트딜 범위

    Player_UI player_UI;
    Player_MOVE playerMove;
    Player_Stat playerStat;

    public bool isESkill; // E 스킬 사용 확인
    public bool isQSkill; // Q 스킬 사용 확인
    public bool NONONO;
    #endregion

    #region 근본
    void Awake()
    {
        playerMove = GetComponentInParent<Player_MOVE>();
        player_UI = FindObjectOfType<Player_UI>();
        playerStat = GetComponentInParent<Player_Stat>();
    }
    void Update()
    {
        if (!playerMove.isAtk)
        {
            // E스킬
            if (Input.GetKeyDown(KeyCode.E) && !isESkill)
            {
                isESkill = true;
                E();
            }
            // Q스킬
            if (Input.GetKeyDown(KeyCode.Q) && !isQSkill)
            {
                isQSkill = true;
                Q();
            }
        }
    }
    #endregion

    private void OnDisable()
    {
        StopAllCoroutines();
        isESkill = false;
        isQSkill = false;
        playerMove.neverMove = false;
    }

    public void Q_setActive_false()
    {
        playerMove.isAtk = false; // 공격을 멈춤
    }
    public void E_setActive_false()
    {
        playerMove.isAtk = false; // 공격을 멈춤
        playerMove.speed = playerMove.defaultSpeed; // 움직임도 다시 제자리로
    }
    void E() // E 스킬 사용
    {
        StartCoroutine(player_UI.E_CoolTime(6));

        playerStat.ATK *= 2;
        playerMove.speed = 3; // 움직임을 3으로
        playerMove.isAtk = true; // 저 공격하고 있어요
        E_effect.SetActive(true); // E 이펙트 실행
        GetComponent<Animator>().SetBool("isAtk", true); // 저 공격하고 있어요
        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.1f);
        playerStat.ATK /= 2;
        yield return new WaitForSeconds(6f);
        isESkill = false; // E스킬도 멈춤
    }

    void Q()
    {
        StartCoroutine(Q_물리());
    }
    IEnumerator Q_물리()
    {
        // 대충 움직임을 멈추고 0.6초 후에 E스킬이랑 모양이 똑같은 스킬을 사용
        Q_effect.SetActive(true);
        StartCoroutine(player_UI.Q_CoolTime(15));
        playerMove.neverMove = true;
        isQSkill = true;
        playerMove.speed = 0f;
        yield return new WaitForSeconds(0.6f);
        playerMove.isAtk = true;
        GetComponent<Animator>().SetBool("isAtk", true);
        E_effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerMove.neverMove = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DotDeal());
    }

    IEnumerator DotDeal()
    {
        int a = 0;
        while (a < 4)
        {
            피해구역.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            a++;
        }
        yield return new WaitForSeconds(7.4f);
        isQSkill = false; // Q스킬도 멈춤
    }


    public void IsAtk() // 애니메이션 이벤트용 함수.
    {
        print("애아래위ㅏ무ㅠㅜㄻ");
        playerMove.isAtk = false;
        GetComponent<Animator>().SetBool("isAtk", playerMove.isAtk);
        playerMove.speed = playerMove.defaultSpeed;
    }
    private void OnEnable()
    {
        playerMove.speed = playerMove.defaultSpeed;
        playerMove.isAtk = false;
        player_UI.Q_Exist();
    }

}
