using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image fade_Image;
    public bool fadeOut;
    public bool fadeOutttt;
    public bool isOutDoor;
    public IEnumerator Fade_In()
    {
        yield return new WaitForSeconds(1f);
        fadeOut = false;
        fadeOutttt = false;
        while (fade_Image.fillAmount > 0f)
        {
            fade_Image.fillAmount -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        // FindObjectOfType<DialogueManager>().YesMove();
        yield return null;
        if (FindObjectOfType<Player_MOVE>()) FindObjectOfType<Player_MOVE>().neverMove = false;
    }
    public IEnumerator Fade_Out()
    {
        if (FindObjectOfType<Player_MOVE>()) FindObjectOfType<Player_MOVE>().neverMove = true;
        print("앵??");
        fade_Image.fillAmount = 0f;
        while (true)
        {
            fade_Image.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.01f);
            if (fade_Image.fillAmount >= 1f) break;
        }
        StartCoroutine(Fade_In());
        fadeOut = true;
        fadeOutttt = true;
        yield return null;
    }

    public IEnumerator FadeOut_Scene(bool YEEEES)
    {
        isOutDoor = YEEEES;
        fade_Image.fillAmount = 0f;
        while (true)
        {
            fade_Image.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.01f);
            if (fade_Image.fillAmount >= 1f) break;
        }
        StartCoroutine(Fade_In());
        fadeOut = true;
        yield return null;
    }
}
