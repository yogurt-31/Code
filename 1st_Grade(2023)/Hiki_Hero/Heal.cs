using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    Player_Stat playerStat;

    private void Awake()
    {
        playerStat = FindObjectOfType<Player_Stat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(HealHP());
    }

    IEnumerator HealHP()
    {
        while(playerStat.HP < playerStat.currentHP * playerStat.Player_LV)
        {
            playerStat.HP++;
            FindObjectOfType<StatManager>().SendStat(playerStat.HP, playerStat.ATK);
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
