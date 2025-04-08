using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Heal,
    Atk
}
public class WitchBuff : MonoBehaviour
{
    public BuffType buffType;
    MawangStat mawangStat;

    bool isfainting;

    private void Awake()
    {
        mawangStat = FindObjectOfType<MawangStat>();
        switch(buffType)
        {
            case BuffType.Heal:
                StartCoroutine(HealBuff());
                break;
            case BuffType.Atk:
                StartCoroutine(AtkBuff());
                break;
        }
    }
    IEnumerator HealBuff()
    {
        while(true)
        {
            if (!FindObjectOfType<MawangStat>().isDeath) break;

            if (!isfainting)
                {
                    while (GetComponent<Enemy_Stat>().HP >= 0)
                    {
                        yield return new WaitForSeconds(5f);
                        if (mawangStat.HP <= mawangStat.currentHP * mawangStat.Enemy_LV - 500)
                        {
                            mawangStat.HP += 500;
                            mawangStat.UpdateHP();
                        }

                    }
                }
            isfainting = true;
            yield return new WaitForSeconds(40f);
            isfainting = false;
        }
        yield return null;
    }

    IEnumerator AtkBuff()
    {
        if(!isfainting) mawangStat.ATK += 50;
        while(true)
        {
            if(GetComponent<Enemy_Stat>().HP <= 0)
            {
                isfainting = true;
                mawangStat.ATK -= 50;
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(30f);
        if(mawangStat.HP > 0f) FullHP();
        isfainting = false;
    }

    void FullHP()
    {
        GetComponent<Enemy_Stat>().HP = GetComponent<Enemy_Stat>().currentHP * GetComponent<Enemy_Stat>().Enemy_LV;
    }
}
