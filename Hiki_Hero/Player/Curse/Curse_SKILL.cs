using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse_SKILL : MonoBehaviour
{
    [SerializeField] Player_MOVE playerMove;
    void Start()
    {
        //playerMove = GetComponentInParent<Player_MOVE>();
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
        FindObjectOfType<Player_UI>().Q_Exist();
    }
}
