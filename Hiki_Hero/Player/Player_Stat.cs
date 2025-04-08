using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    public int Player_LV;
    public int HP;
    public int ATK;
    public int currentHP = 1000;

    private int currentATK = 100;

    StatManager stat;

    private void Awake()
    {
        stat = FindObjectOfType<StatManager>();

        print(PlayerPrefs.GetInt("레벨"));
        Player_LV = PlayerPrefs.GetInt("레벨");
        UpdateStat();
    }
    private void Update()
    {
        stat.SendStat(HP, ATK);

        if(HP <= 0f)
        {
            FindObjectOfType<Player_UI>().DiePanel();
        }
    }

    public void UpdateStat()
    {
        HP = currentHP * Player_LV;
        ATK = currentATK * Player_LV;
    }
    public void UpdateAtkStat()
    {
        ATK = currentATK * Player_LV;
    }
    public void LevelUp()
    {
        Player_LV += 1;
        PlayerPrefs.SetInt("레벨", Player_LV);
        UpdateStat();
    }
}
