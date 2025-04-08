using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI atk;
    public void SendStat(int _hp, int _atk)
    {
        hp.text = "HP : " + _hp.ToString();
        atk.text = "ATK : " + _atk.ToString();
    }
}
