using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAtk : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(CoolTime());
        StartCoroutine(GetComponentInParent<Lazer>().LazerDeal());
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Lazer>().canAtk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<Lazer>().canAtk = false;
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(4.9f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
