using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class NoticePanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private StartPanelSystem startPanelSystem;
    private Image noticePanelImage;
    private Image headPhonesBackgroundImage;
    private Image headPhonesImage;
    private TextMeshProUGUI noticeText;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        noticePanelImage = GetComponent<Image>();
        headPhonesBackgroundImage = noticePanelImage.transform.Find("Background").GetComponent<Image>();
        headPhonesImage = headPhonesBackgroundImage.transform.GetChild(0).GetComponent<Image>();
        noticeText = noticePanelImage.transform.Find("NoticeText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        StartCoroutine(ImageFade(headPhonesBackgroundImage, 1f, true));
        StartCoroutine(ImageFade(noticeText, 1f, true));
        StartCoroutine(DelayCoroutine(2f));
    }


    IEnumerator ImageFade(Image img, float fadeTime, bool isFadeIn)
    {
        float startValue, endValue;
        if(isFadeIn)
        {
            startValue = 0f;
            endValue = 1f;
        }
        else
        {
            startValue = 1f;
            endValue = 0f;
        }
        float currentTime = 0f;
        while(currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float lerp = Mathf.Lerp(startValue, endValue, currentTime / fadeTime);
            img.color = new Color(img.color.r, img.color.g, img.color.b, lerp);
            yield return null;
        }
        
    }
    IEnumerator ImageFade(TextMeshProUGUI text, float fadeTime, bool isFadeIn)
    {
        float startValue, endValue;
        if (isFadeIn)
        {
            startValue = 0f;
            endValue = 1f;
        }
        else
        {
            startValue = 1f;
            endValue = 0f;
        }
        float currentTime = 0f;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float lerp = Mathf.Lerp(startValue, endValue, currentTime / fadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, lerp);
            yield return null;
        }
    }

    IEnumerator DelayCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(ImageFade(noticeText, 1f, false));
        StartCoroutine(ImageFade(headPhonesBackgroundImage, 1f, false));
        yield return new WaitForSeconds(1f);
        headPhonesImage.gameObject.SetActive(false);
        StartCoroutine(ImageFade(noticePanelImage, 1f, false));
        yield return new WaitForSeconds(1f);
        noticePanelImage.gameObject.SetActive(false);
        //startPanelSystem.OpenNamePanel();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        noticePanelImage.gameObject.SetActive(false);
        //startPanelSystem.OpenNamePanel();
    }
}
