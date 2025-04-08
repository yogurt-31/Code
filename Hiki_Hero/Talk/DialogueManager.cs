using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialoguePanel;
    Fade fade;

    string[] dialogue;
    public string FileName;

    public int i = 0;

    public bool isTalk;
    public bool isEnd;
    public bool nextDialogue;
    public bool storySkip;
    public bool happy;


    private void Start()
    {
        fade = FindObjectOfType<Fade>();
        dialogue = File.ReadAllLines(FileName);
        
    }
    public void DialogueSystem(int num)
    {
        //isEnd = false;
        i = num;
        if(!isTalk)
            StartCoroutine(TextAnimation());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1) && isTalk)
        {
            storySkip = true;
        }
    }

    IEnumerator TextAnimation() // 텍스트 나오게 하는거
    {
        print("실행해야디");
        if(FindObjectOfType<Player_MOVE>())
        {
            FindObjectOfType<Player_MOVE>().neverMove = true;
        }
        isTalk = true;
        happy = true;
        dialoguePanel.SetActive(true);
        while (true)
        {
            string sentence = dialogue[i];
            if (sentence == ",") break;
            i++;
            if(!storySkip)
            {
                dialogueText.text = "";
                foreach (char text in sentence)
                {
                    dialogueText.text += text;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
        isEnd = true;
        print("내가 더빠름름 ㅅㄱ");
        happy = false;
        dialoguePanel.SetActive(false);
        if (FindObjectOfType<Player_MOVE>())
        {
            FindObjectOfType<Player_MOVE>().neverMove = false;
            FindObjectOfType<Player_MOVE>().SpeedDefault();
        }
        yield return new WaitForSeconds(1f);
        isEnd = false;
        isTalk = false;
        storySkip = false;
    }

    public void StorySkip()
    {
        storySkip = true;
    }
}
