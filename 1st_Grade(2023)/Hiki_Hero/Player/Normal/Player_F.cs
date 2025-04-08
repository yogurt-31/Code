using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_F : MonoBehaviour
{
    [SerializeField] private Image F_panel; // 나가기 판넬
    public bool isDoor; // 문인지 아닌지 확인하는것.
    public bool isOutDoor;
    private void OnTriggerStay2D(Collider2D collision)
    {
        // 문 트리거 안에 있으면 판넬 보여줌
        if (collision.CompareTag("Village-1_Door"))
        {
            isDoor = true;
            isOutDoor = true;
            F_panel.gameObject.SetActive(true);
        }
        if(collision.CompareTag("Dunjeon_Door"))
        {
            if(FindObjectOfType<DialogueManager>().nextDialogue)
            {
                isDoor = true;
                isOutDoor = false;
                F_panel.gameObject.SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // 문 트리거 밖에 있으면 판넬 숨김
        if (collision.CompareTag("Village-1_Door"))
        {
            isDoor = false;
            isOutDoor = false;
            F_panel.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Dunjeon_Door"))
        {
            isDoor = false;
            isOutDoor = false;
            F_panel.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        // 문 안에 있을 때 F를 누르면 씬전환
        if(isDoor)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(FindObjectOfType<Fade>().FadeOut_Scene(isOutDoor));
            }
        }
    }
}
