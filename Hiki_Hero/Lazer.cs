using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    bool canRotate;
    public bool canAtk;
    private void OnEnable()
    {
        canRotate = true;
        StartCoroutine(CoolTime());
    }
    void Update()
    {
        if(canRotate)
            transform.Rotate(new Vector3(0, 0, 40f * Time.deltaTime));
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.SetActive(false);
        }
    }
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(5f);
        canRotate = false;
    }

    

    public IEnumerator LazerDeal()
    {

        while(true)
        {
            if(canAtk)
                FindObjectOfType<Player_Stat>().HP -= (int)(FindObjectOfType<MawangStat>().ATK * 0.1f);
            if (!FindObjectOfType<Mawang>().isAtk) break;
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
}
