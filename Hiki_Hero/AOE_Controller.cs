using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Controller : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
