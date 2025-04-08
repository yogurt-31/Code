using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum 캐릭터속성
{
    Normal,
    Heal,
    Biology,
    Ghost,
    Curse
}

public class CharacterManager : MonoBehaviour
{
    public 캐릭터속성 캐릭터속성;

    public int i;
    public string 속성;
    private bool isPanel;
    private string[] type = {"Normal", "Heal", "Biology", "Ghost", "Curse" };

    [SerializeField] private GameObject player_Panel;

    private void Awake()
    {

        속성 = PlayerPrefs.GetString("캐릭터속성");
        for (i = 0; i < type.Length; i++)
        {
            if(type[i] == 속성)
            {
                GameObject.Find("Player").transform.GetChild(i).gameObject.SetActive(true);
                FindObjectOfType<Player_UI>().ChangeInfo(i);
                break;
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isPanel)
        {
            FindObjectOfType<Player_UI>().canEsc = false;
            isPanel = true;
            player_Panel.SetActive(true);
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.C) && isPanel)
        {
            FindObjectOfType<Player_UI>().canEsc = true;
            isPanel = false;
            player_Panel.SetActive(false);
            Time.timeScale = 1;
        }
        if(isPanel)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Normal_Btn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Heal_Btn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Biology_Btn();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Ghost_Btn();
            }
        }

    }

    public void Normal_Btn()
    {
        캐릭터속성 = 캐릭터속성.Normal;
        ChangeType();
    }
    public void Heal_Btn()
    {
        캐릭터속성 = 캐릭터속성.Heal;
        ChangeType();
    }
    public void Biology_Btn()
    {
        캐릭터속성 = 캐릭터속성.Biology;
        ChangeType();
    }
    public void Ghost_Btn()
    {
        캐릭터속성 = 캐릭터속성.Ghost;
        ChangeType();

    }
    public void Curse_Btn()
    {
        캐릭터속성 = 캐릭터속성.Curse;
        ChangeType();
    }

    void ChangeType()
    {
        FindAnyObjectByType<Player_Skill>().SetActiveEff();

        for (int i = 0; i < type.Length; i++)
        {
            GameObject.Find("Player").transform.GetChild(i).gameObject.SetActive(false);
        }
        속성 = 캐릭터속성.ToString();
        PlayerPrefs.SetString("캐릭터속성", 속성);
        for (i = 0; i < type.Length; i++)
        {
            if (type[i] == 속성)
            {
                GameObject.Find("Player").transform.GetChild(i).gameObject.SetActive(true);
                GameObject.Find("Player").transform.GetChild(i).position =
                    FindObjectOfType<Player_Pos>().transform.position;
                FindObjectOfType<Player_UI>().ChangeInfo(i);

                FindAnyObjectByType<Camera_Move>().ChangeTarget();
                break;
            }
        }

        FindObjectOfType<Player_Stat>().UpdateAtkStat();
        FindObjectOfType<Player_UI>().E.fillAmount = 1;
        FindObjectOfType<Player_UI>().Q.fillAmount = 1;
        FindObjectOfType<Player_UI>().Dash.fillAmount = 1;
    }
}
