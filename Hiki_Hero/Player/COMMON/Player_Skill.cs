using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    [SerializeField] private int skill_Count;

    public void SetActiveEff()
    {
        print("�¿�Ƽ�� �۵�.");
        for (int i = 0; i < skill_Count; i++)
        {
            GameObject.Find("Player_Skill").transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
