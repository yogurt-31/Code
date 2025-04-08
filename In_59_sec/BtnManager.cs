using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel;
    Timer timer;

    public bool isMenu = false;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        timer = FindObjectOfType<Timer>();
    }
    public void StartBtn()
    {
        // 메뉴 화면(스테이지 선택 화면)으로 이동
        SceneManager.LoadScene("MenuScene");
    }
    
    public void ExitBtn()
    {
        // 게임 종료 버튼
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Stage_1Btn()
    {
        // 스테이지 1로 이동
        SceneManager.LoadScene("Stage-1");
    }

    public void Stage_2Btn()
    {
        // 스테이지 1로 이동
        SceneManager.LoadScene("Stage-2");
    }

    public void StartSceneBtn()
    {
        // 스타트 화면으로 이동
        SceneManager.LoadScene("StartScene");
    }

    public void GameMenuBtn()
    {
        if (!isMenu)
        {
            // 게임 메뉴 판넬을 꺼냄.
            isMenu = true;
            MenuPanel.SetActive(true);
            timer.StopTimer();
            audioSource.Pause();
        }
        else
        {
            isMenu = false;
            MenuPanel.SetActive(false);
            timer.TimerStart();
            audioSource.Play();
        }
    }
}
