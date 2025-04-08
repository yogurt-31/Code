using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class SongLengthSystem : MonoBehaviour
{
    [SerializeField] private Image songLengthPointer;
    [SerializeField] private Image songPrograssBar;
    [SerializeField] private Image fillbar;
    private float canvasWidth;
    private float songTotalTime;

    private bool isSongPlaying = false;

    private float barLenght;

    private RhythmGameManager gameManager;
    private AudioSource songTime;

    private void Awake()
    {
        canvasWidth = GetComponentInParent<RectTransform>().rect.width;
        songTotalTime = Information.Instance.currentSong.AudioFile.length;
        gameManager = FindObjectOfType<RhythmGameManager>();
        songTime = gameManager.BGMSource;
    }

    private void Start()
    {
        gameManager.GameStartEvent += SongStart;
        gameManager.GameStopEvent += SongEnd;
        barLenght = songPrograssBar.rectTransform.rect.width;
        fillbar.fillAmount = 0f;
    }

    private void SongEnd()
    {
        float lerp = Mathf.Lerp(0f, canvasWidth, 1f);
        Vector3 vec = new Vector3(lerp, songLengthPointer.rectTransform.anchoredPosition.y);
        songLengthPointer.rectTransform.anchoredPosition = vec;
    }

    private void SongStart()
    {
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2f);
        isSongPlaying = true;
    }

    private void Update()
    {
        if(isSongPlaying)
        {
            if(songTime.time == 0f)
            {
                float lerp = Mathf.Lerp(0f, canvasWidth, 1f);
                Vector3 vec = new Vector3(lerp, songLengthPointer.rectTransform.anchoredPosition.y);
            }
            else
            {
                float lerp = Mathf.Lerp(0f, barLenght, songTime.time / songTime.clip.length);
                Vector3 vec = new Vector3(lerp, songLengthPointer.rectTransform.anchoredPosition.y);
                songLengthPointer.rectTransform.anchoredPosition = vec;
                fillbar.fillAmount = lerp / barLenght;
            }
        }
    }

    private void OnDestroy()
    {
        gameManager.GameStartEvent -= SongStart;
        gameManager.GameStopEvent -= SongEnd;
    }
}
