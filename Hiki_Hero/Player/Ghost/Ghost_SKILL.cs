using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_SKILL : MonoBehaviour
{
    [SerializeField] Player_MOVE playerMove;
    [SerializeField] Player_Stat _playerstate;

    Player_UI player_UI;
    void Awake()
    {
        player_UI = FindObjectOfType<Player_UI>();
        // playerMove = GetComponentInParent<Player_MOVE>();
    }
    public void IsAtk() // 애니메이션 이벤트용 함수.
    {
        playerMove.isAtk = false;
        GetComponentInChildren<Animator>().SetBool("isAtk", playerMove.isAtk);
        playerMove.speed = playerMove.defaultSpeed;
    }
    #region 스킬 만든거

    //ESkill
    [SerializeField] private float tickDelay = 0.5f;
    [SerializeField] private int hpLostValue = 1;
    [SerializeField] private int AtkFoundValue = 2;
    [SerializeField] private float ESkillDuration = 8f;
    [SerializeField] private float ESkillCoolTime = 15f;
    public bool SoulState = false;
    private bool canSkillE = true;
    private float timer;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillEHandler();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SkillQHandler();
        }
        if(SoulState) player_UI.Q_Exist();
    }

    private void SkillEHandler()
    {
        if (canSkillE == true)
            StartCoroutine("SkillE");
    }

    private void SkillQHandler()
    {
        if (SoulState == true)
            SkillQ();
    }

    private void SkillQ()
    {
        Debug.Log($"{Time.time} Qskill Start");

        SoulState = false;
        StartCoroutine(playerMove.Dash(0.2f));
        player_UI.Q_NoExist();

        Debug.Log($"{Time.time} Qskill End");
        Debug.Log($"{Time.time} Eskill Cooltime End");
    }

    private IEnumerator SkillE()
    {
        StartCoroutine(player_UI.E_CoolTime(17));
        Debug.Log($"{Time.time} Eskill Start");
        canSkillE = false;
        SoulState = true;
        timer = ESkillDuration;

        var startATK = _playerstate.ATK;

        while (true)
        {
            if (timer <= 0) break;
            yield return new WaitForSeconds(tickDelay);
            timer -= tickDelay;
            _playerstate.HP -= hpLostValue;
            _playerstate.ATK += AtkFoundValue;
        }
        yield return new WaitForSeconds(2f);
        Debug.Log($"{Time.time} Eskill End");
        _playerstate.ATK = startATK;
        SoulState = false;

        yield return new WaitForSeconds(7.1f);
        canSkillE = true;
        Debug.Log($"{Time.time} Eskill Cooltime End");
    }

    #endregion
    private void OnEnable()
    {
        playerMove.speed = playerMove.defaultSpeed;
        playerMove.isAtk = false;
        player_UI.Q_NoExist();
    }
}
