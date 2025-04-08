using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Player_UI : MonoBehaviour
{
    public Image Dash;
    public Image E;
    public Image Q;
    public Image C;

    public TextMeshProUGUI type_Text;
    public TextMeshProUGUI E_Text;
    public TextMeshProUGUI Q_Text;

    public Sprite[] type_Images;

    public GameObject Die_Panel;
    public GameObject Esc_Panel;

    bool isPanel;
    public bool canEsc;

    private void Awake()
    {
        Time.timeScale = 1f;
        canEsc = true;
    }

    string[] types = { "물리", "치유", "생물", "영혼", "저주" };
    string[] E_Info =
    {
        "E : 전방을 향해 휘두릅니다.\n<쿨타임 : 6초>",
        "E : 신성 영역을 생성해 초당 전체 HP의 5%의 HP를 회복합니다.\n<쿨타임 : 15초>",
        "E : 전방을 향해 피닉스를 날립니다.\n<쿨타임 : 7초>",
        "E : 영혼 상태를 부여하며, 부여될 시 초당 체력을 잃고 공격력이 증가합니다.\n<쿨타임 : 15초>",
        "E : 적에게 부적을 부착하여 적 비례 당 피해를 줍니다.\n<쿨타임 : 7초>"
    };

    string[] Q_Info =
    {
        "Q : 전방을 향해 힘을 모아 휘두르고, 피해 구역을 생성합니다.\n<쿨타임 : 15초>",
        "Q : 보호막을 생성해 피해를 받지 않습니다.\n<쿨타임 : 20초>",
        "Q : 복어 폭탄을 설치하여 적이 닿을 경우 큰 피해와 기절 효과를 받습니다.\n<쿨타임 : 13초>",
        "Q : 영혼 상태를 끝내고 전방을 향해 돌진하며 피해를 입힙니다.\n<쿨타임 : 없음>",
        "Q : 적에게 부적을 부착하여 큰 피해를 주고, 적 비례 당 HP를 회복합니다.\n<쿨타임 : 20초>"
    };

    public IEnumerator DashCoolTime()
    {
        Dash.fillAmount = 0f;
        while(Dash.fillAmount < 1f)
        {
            Dash.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator E_CoolTime(int time)
    {
        E.fillAmount = 0f;
        while(E.fillAmount < 1f)
        {
            E.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f*time);
        }
    }

    public IEnumerator Q_CoolTime(int time)
    {
        Q.fillAmount = 0f;
        while (Q.fillAmount < 1f)
        {
            Q.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f * time);
        }
    }
    public void Q_Exist()
    {
        print("예");
        Q.enabled = true;
    }
    public void Q_NoExist()
    {
        Q.enabled = false;
    }

    public void ChangeInfo(int i)
    {
        C.sprite = type_Images[i];
        type_Text.text = types[i];
        E_Text.text = E_Info[i];
        Q_Text.text = Q_Info[i];
    }

    public void RestartBtn()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Die_Panel.SetActive(false);
    }

    public void DiePanel()
    {
        Die_Panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPanel && canEsc) 
        {
            isPanel = true;
            OptionPanel(isPanel);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPanel && canEsc)
        {
            isPanel = false;
            OptionPanel(isPanel);
        }
    }

    private void OptionPanel(bool setActive)
    {
        if (setActive) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        Esc_Panel.SetActive(setActive);
    }

    public void ExitBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1f;
        Esc_Panel.SetActive(false);
        isPanel = false;
    }
}
