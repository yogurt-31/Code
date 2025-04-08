using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScarletCharacter : MonoBehaviour
{
    [SerializeField] private int first_Character_Number;
    [SerializeField] private int last_Character_Number;

    [SerializeField] private GameObject[] Characters;

    bool isCreate;

    private void FixedUpdate()
    {
        if (!isCreate && GetComponent<DialogueTrigger>().isPlayer)
        {
            isCreate = true;
            LoadCharacter(isCreate, first_Character_Number, last_Character_Number);
        }

        if (FindObjectOfType<DialogueManager>().isEnd && isCreate)
        {
            isCreate = false;
            LoadCharacter(isCreate, first_Character_Number, last_Character_Number);
        }
    }

    public void LoadCharacter(bool setActive, int firstNum, int lastNum)
    {
        for (int i = firstNum; i <= lastNum; i++)
        {
            print(gameObject.name + "에서 FOR문 실행");
            Characters[i].SetActive(setActive);
        }
    }
}
