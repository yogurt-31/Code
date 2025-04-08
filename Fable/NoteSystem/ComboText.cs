using System.Collections;
using TMPro;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboText;
    private PlayerAnimaionControl playerAnimaionControl;
    private IEnumerator popupTextCo;

    private int combo = 0;

    public bool isAP = true;
    private bool isPC = true;
    private bool isRunning = false;

    private void Start()
    {
        playerAnimaionControl = FindObjectOfType<PlayerAnimaionControl>();
        popupTextCo = PopupText();
    }

    public void ShowCombo(bool isCombo)
    {
        if (!isCombo)
        {
            isPC = false;
            combo = 0;
            comboText.text = "";
            isRunning = false;
            playerAnimaionControl.Running(isRunning);
        }
        else
        {
            ++combo;
            if (isRunning == false && combo >= 25)
            {
                isRunning = true;
                playerAnimaionControl.Running(isRunning);
            }
            if (popupTextCo != null)
            {
                StopCoroutine(popupTextCo);
            }
            popupTextCo = PopupText();
            StartCoroutine(popupTextCo);
        }
    }

    private IEnumerator PopupText()
    {
        SelectTextColor();
        Vector2 textRectTrm = comboText.rectTransform.anchoredPosition;
        comboText.text = combo.ToString();
        float currentTime = 0f;
        while (currentTime < 0.3f)
        {
            currentTime += Time.deltaTime;
            float lerp = Mathf.Lerp(360f, 400f, currentTime / 0.3f);
            float sizelerp = Mathf.Lerp(1.5f, 1.0f, currentTime / 0.3f);
            textRectTrm = new Vector3(textRectTrm.x, lerp, 0);
            comboText.rectTransform.localScale = Vector2.one * sizelerp;
            comboText.rectTransform.anchoredPosition = textRectTrm;
            yield return null;
        }
        yield return null;
    }

    private void SelectTextColor()
    {
        if (isAP)
        {
            comboText.color = new Color(1, 0.7f, 0.9f, 1f);
        }
        else if (isPC)
        {
            comboText.color = new Color(0.6f, 0.8f, 1f, 1f);
        }
        else
        {
            comboText.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
