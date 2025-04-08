using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void GameStartBtn()
    {
        SceneManager.LoadScene("Prologue");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("레벨", 1);
        PlayerPrefs.SetString("캐릭터속성", "Normal");
        //SceneManager.LoadScene("Scarlet");
    }
    public void GameExitBtn()
    {
        Application.Quit();
    }

    public void DataReset()
    {
        PlayerPrefs.SetInt("레벨", 1);
        PlayerPrefs.SetString("캐릭터속성", "Normal");
    }
    public void 치트()
    {
        PlayerPrefs.SetInt("레벨", 100);
        PlayerPrefs.SetString("캐릭터속성", "Normal");
    }
}
