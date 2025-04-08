using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float x;
    public float y;
    public bool canAtk;

    private void OnEnable()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(AtkTime());
    }
    void Update()
    {
        Vector3 vec = new Vector3(x, y, 0);
        transform.position += vec * 13f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canAtk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canAtk = false;
    }

    IEnumerator AtkTime()
    {
        float damage = 0.1f;
        while(true)
        {
            if(canAtk && FindObjectOfType<Mawang>().isAtk)
            {
                canAtk = false;
                FindObjectOfType<Player_Stat>().HP -= (int)(FindObjectOfType<MawangStat>().ATK * damage);
                yield return null;
            }
            damage += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
