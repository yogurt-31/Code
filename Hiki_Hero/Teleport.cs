using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject warp_Panel;
    [SerializeField] private Sprite[] type_Images;

    public bool isPanel;

    public int storyNum;


    private void Update()
    {
        if (FindObjectOfType<DialogueManager>().i <= 71 || FindObjectOfType<DialogueManager>().happy) return;

        if(Input.GetKeyDown(KeyCode.M) && !isPanel)
        {
            FindObjectOfType<Player_UI>().canEsc = false;
            PanelOn();
        }
        else if(Input.GetKeyDown(KeyCode.M) && isPanel)
        {
            FindObjectOfType<Player_UI>().canEsc = true;
            PanelOff();
        }
        
    }
    private void PanelOn()
    {
        isPanel = true;
        warp_Panel.SetActive(true);
    }
    private void PanelOff()
    {
        isPanel = false;
        warp_Panel.SetActive(false);
    }

    public void Scarlet_Warp()
    {
        SceneManager.LoadScene("Scarlet");
        PanelOff();
    }

    public void Idas_Warp()
    {
        SceneManager.LoadScene("Idas");
        PanelOff();
    }

    public void WitchWorld()
    {
        SceneManager.LoadScene("Walpurgis");
        PanelOff();
    }
}
