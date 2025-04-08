using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float limitTime;
    public TextMeshProUGUI text_Timer;
    IEnumerator enumerator;

    private void Start()
    {
        limitTime = 59;
        enumerator = StartTimer();
        StartCoroutine(enumerator);
    }

    public void TimerStart() => StartCoroutine(enumerator);
    public void StopTimer() => StopCoroutine(enumerator);
    public IEnumerator StartTimer()
    {
        while(limitTime > 0)
        {
            limitTime -= Time.deltaTime;
            text_Timer.text = "Time : " + (int)limitTime;
            yield return null;
        }
    }
}
