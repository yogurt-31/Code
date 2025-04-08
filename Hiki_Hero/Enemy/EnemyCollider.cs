using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    Enemy_1 enemy;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy_1>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemy.isAtk & !enemy.isAtkCoolTime)
        {
            if (GetComponentInParent<Enemy_Stat>().isDeath) return;
            enemy.isAtk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.isAtk = false;
    }
}
