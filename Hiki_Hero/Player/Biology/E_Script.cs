using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Script : MonoBehaviour
{
    public float speed;
    int dir;

    

    private void OnEnable()
    {
        dir = FindObjectOfType<Player_MOVE>().isFlip;
        StartCoroutine(Move());
        Vector3 vec = new Vector3(-0.2f, -1.6f, 0);
        transform.position = FindObjectOfType<Player_MOVE>().transform.position + vec;
    }
    void Update()
    {
        gameObject.transform.position += Vector3.right * speed * Time.deltaTime
            * dir;

        if(dir == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (dir == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
    IEnumerator Move()
    {
        print("ÇìÇì");
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("isDisable", true);

        yield return null;
    }

    public void anime()
    {
        print("¾ÆÀ×¤·");
        gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("isDisable", false);
    }
}
