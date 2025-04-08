using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biology_SKILL : MonoBehaviour
{
    [SerializeField] private E_Script eScript;
    [SerializeField] private GameObject blowFish;
    Player_UI player_UI;
    Player_MOVE playerMove;

    bool isESkill;
    bool isQSkill;
    void Awake()
    {
        player_UI = FindObjectOfType<Player_UI>();
        playerMove = GetComponentInParent<Player_MOVE>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isESkill)
        {
            isESkill = true;
            StartCoroutine(E());
        }
        if(Input.GetKeyDown(KeyCode.Q) && !isQSkill)
        {
            isQSkill = true;
            StartCoroutine(Q());
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        isESkill = false;
    }
    IEnumerator Q()
    {
        StartCoroutine(player_UI.Q_CoolTime(13));
        blowFish.SetActive(false);
        blowFish.SetActive(true);
        yield return new WaitForSeconds(13.1f);
        isQSkill = false;
    }
    IEnumerator E()
    {
        StartCoroutine(player_UI.E_CoolTime(7));
        eScript.SetActive();
        yield return new WaitForSeconds(7.1f);
        isESkill = false;
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
