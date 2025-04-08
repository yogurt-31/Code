using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowfish : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject deal;
    void OnEnable()
    {
        transform.position = target.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            deal.SetActive(true);
            StartCoroutine(SetActive());
        }
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(0.1f);
        deal.SetActive(false);
        gameObject.SetActive(false);
    }
}
