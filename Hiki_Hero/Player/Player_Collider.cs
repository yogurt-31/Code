using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collider : MonoBehaviour
{
    public bool canAtk;
    private void OnTriggerStay2D(Collider2D collision)
    {
        canAtk = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canAtk = false;
    }
}
