using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWitch : MonoBehaviour
{
    [SerializeField] private int first_Witch_Number;
    [SerializeField] private int last_Witch_Number;
    
    [SerializeField] private GameObject[] witches;

    public bool isCreate;

    private void FixedUpdate()
    {
        if(!isCreate && GetComponent<DialogueTrigger>().isPlayer)
        {
            isCreate = true;
            LoadWitch(isCreate, first_Witch_Number, last_Witch_Number);
        }

        if(FindObjectOfType<DialogueManager>().isEnd && isCreate)
        {
            isCreate = false;
            LoadWitch(isCreate, first_Witch_Number, last_Witch_Number);
        }
    }

    public void LoadWitch(bool setActive,  int firstNum, int lastNum)
    {
        for (int i = firstNum; i <= lastNum; i++)
        {
            witches[i].SetActive(setActive);
        }
    }
}
