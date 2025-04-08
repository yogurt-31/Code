using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeOX
{
    YesFade,
    NoFade
}
public enum OnceOX
{
    Yes,
    No
}
public enum TeleportOX
{
    Yes,
    No
}
public enum NextDialogueOX
{
    Yes,
    No
}
public class DialogueTrigger : MonoBehaviour
{
    public FadeOX fade;
    public OnceOX once;
    public TeleportOX warp;
    public NextDialogueOX next;

    public int num;
    public bool isPlayer;
    public bool isOnce;
    public bool isCollider = true;
    public bool isRun;

    DialogueManager dialogue;
    Fade fade_Script;
    [SerializeField] Collider2D box_collider;

    private void Awake()
    {
        isCollider = true;
        dialogue = FindObjectOfType<DialogueManager>();
        fade_Script = FindObjectOfType<Fade>();
        print(gameObject.name + "의 숫자는 " + num);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayer = false;
    }

    void Update()
    {
        if (!dialogue.nextDialogue && !isRun)
        {
            switch (next)
            {
                case NextDialogueOX.Yes:
                    box_collider.enabled = false;
                    break;
                case NextDialogueOX.No:
                    box_collider.enabled = true;
                    break;
            }
        }
        else if(dialogue.nextDialogue && !isRun)
        {
            switch (next)
            {
                case NextDialogueOX.Yes:
                    box_collider.enabled = true;
                    break;
                case NextDialogueOX.No:
                    box_collider.enabled = false;
                    break;
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(ddd());
    }

    IEnumerator ddd()
    {
        while(true)
        {
            print(gameObject.name + "코루틴 돌고는 있니?");
            if (dialogue.isEnd)
            {
                if (!isOnce && isRun)
                {
                    isOnce = true;
                    OnceDialogue();
                    isCollider = false;
                    print("내가 더빠름 ㅅㄱ");
                    break;
                }
            }
            else if (isPlayer && !dialogue.isTalk)
            {
                isRun = true;
                box_collider.enabled = false;
                print(gameObject.name + ": " + num + "보냇쪄염");
                dialogue.DialogueSystem(num);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
    void OnceDialogue()
    {
        if (fade == FadeOX.YesFade)
        {
            StartCoroutine(fade_Script.Fade_Out());
            fade = FadeOX.NoFade;
        }
        if(TeleportOX.Yes == warp)
        {
            if(GetComponent<DialogueTeleport>())
                StartCoroutine(GetComponent<DialogueTeleport>().Teleport());
        }
    }
}
