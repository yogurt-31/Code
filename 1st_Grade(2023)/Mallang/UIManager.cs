
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject canvas;
    private GameObject esc_Panel;
    private GameManager gameManager;

    static public UIManager Instance;

    bool isEsc;

    private void Awake()
    {
        Instance = this;
        gameManager = FindObjectOfType<GameManager>();
        esc_Panel = canvas.transform.Find("Option_Panel").gameObject;
    }

    public void Option_Button() // ���� ��� �ɼ� ��ư.
    {
        if (!isEsc) PanelOn();
        else PanelOff();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isEsc)
        {
            PanelOn();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isEsc)
        {
            PanelOff();
        }
    }

    private void PanelOff()
    {
        isEsc = false;
        esc_Panel.SetActive(false);
        FindObjectOfType<CameraControl>().onRotation = true;
        gameManager.MouseOff();
        Time.timeScale = 1f;
    }

    private void PanelOn()
    {
        isEsc = true;
        esc_Panel.SetActive(true);
        gameManager.MouseOn();
        FindObjectOfType<CameraControl>().onRotation = false;
        Time.timeScale = 0f;
    }
    
    public void Continue_Button()
    {
        PanelOff();
    }

    public void Exit_Button()
    {
        print("�� �̵��ؾߵǴµ� ���� �����Ͽ�");
        // ���� �� �̵� �ϴ� �ڵ� �ۼ��ҵ�.
    }
}
