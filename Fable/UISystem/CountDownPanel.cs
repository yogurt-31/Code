using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CountDownPanel : MonoBehaviour
{
    private RhythmGameManager gameManager;

    [SerializeField] private Image value;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        gameManager = FindObjectOfType<RhythmGameManager>();
    }

    private void OnEnable()
    {
        value.fillAmount = 1;
        StartCoroutine(CountDown());
        DOTween.To(() => value.fillAmount, x => value.fillAmount = x, 0, 3f).SetEase(Ease.Linear).SetUpdate(true);
    }

    private IEnumerator CountDown()
    {
        for(int i = 3; i > 0; --i)
        {
            countText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        gameObject.SetActive(false);
        Time.timeScale = 1;
        gameManager.BGMSource.pitch = 1;
    }
}
