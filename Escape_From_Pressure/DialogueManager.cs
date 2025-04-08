using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KoreanTyper;

public class DialogueManager : MonoBehaviour
{
    string originText;
    [SerializeField] private TextMeshProUGUI _objNameText;
    [SerializeField] private TextMeshProUGUI objExplainText;
    [SerializeField] private GameObject _dialoguePanel;

    private Inventory _inventory;

    [SerializeField] private bool _iSeeYou;
    public bool canTyping = true;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }
    void Start()
    {
        originText = objExplainText.text;
        objExplainText.text = "";
    }
    public IEnumerator TypingRoutine(string objectNameText, string objectExplainText, PlayerCheck playerCheck)
    {
        if(canTyping && objectExplainText != "")
        {
            if (playerCheck._objType == ObjectType.offeringObject) playerCheck.OfferingBoxObject();

            canTyping = false;
            _dialoguePanel.SetActive(true);
            _objNameText.text = objectNameText;
            originText = objectExplainText;
            int typingLength = originText.GetTypingLength();
            print(typingLength);

            for (int index = 0; index <= typingLength; index++)
            {
                objExplainText.text = originText.Typing(index);

                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.5f);
            playerCheck.isTyping = false;
            _dialoguePanel.SetActive(false);
            if (playerCheck._objType == ObjectType.getObject) playerCheck.GetObject();
            else if (playerCheck._objType == ObjectType.fakeObject) playerCheck.TrickObject();
            else if (playerCheck._objType == ObjectType.imageObject) playerCheck.ImageObject();
            else if (playerCheck._objType == ObjectType.eyeTrickObject) playerCheck.IrisChangeObject(true);
            else if (playerCheck._objType == ObjectType.tvObject) FindObjectOfType<TV_MaterialController>().PlayNoize();
            if (playerCheck._objType == ObjectType.doorObject)
            {
                print("삐익");
                if(_inventory.hasObject[0])
                {
                    if(!_iSeeYou && !DoorAnimationController.Instance._animator.GetBool("Stage2")) DoorAnimationController.Instance.DoorOpen(true);
                    else
                    {
                        DoorAnimationController.Instance.StageTwoDoorOpen(true);
                        _iSeeYou = false;
                    }
                }
            }
            canTyping = true;
        }
        else if(objectExplainText == "")
        {
            playerCheck.isTyping = false;
            if (playerCheck._objType == ObjectType.getObject) playerCheck.GetObject();
            else if (playerCheck._objType == ObjectType.fakeObject) playerCheck.TrickObject();
            else if (playerCheck._objType == ObjectType.imageObject) playerCheck.ImageObject();
            else if (playerCheck._objType == ObjectType.offeringObject) playerCheck.OfferingBoxObject();
            else if (playerCheck._objType == ObjectType.eyeTrickObject) playerCheck.IrisChangeObject(true);
            else if (playerCheck._objType == ObjectType.tvObject) FindObjectOfType<TV_MaterialController>().PlayNoize();
            if (playerCheck._objType == ObjectType.doorObject)
            {
                
                if (_inventory.hasObject[0])
                {

                    if (!_iSeeYou) {
                        print("삐익");
                        DoorAnimationController.Instance.DoorOpen(true);
                    } 
                    else
                    {
                        DoorAnimationController.Instance.StageTwoDoorOpen(true);
                        _iSeeYou = false;
                    }
                }
            }
            canTyping = true;
        }
    }
}
