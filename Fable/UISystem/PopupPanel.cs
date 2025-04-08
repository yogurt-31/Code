using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType
{
    InformationConsent,
    ServerInspection,
    EqualIDorFailPassword,
    NotID,
    NoUpdate
}

public class PopupPanel : PanelSystem
{
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptTMP;
    [SerializeField] private Image noTouchZone;

    public PopupType popupType;

    private StartPanelSystem startPanel;

    public void SettingPanel(PopupType popup)
    {
        noTouchZone.gameObject.SetActive(true);
        popupType = popup;
        switch (popupType)
        {
            case PopupType.InformationConsent:
                titleTMP.text = "개인정보처리방침";
                descriptTMP.text = "<color=#AAAAFF><link=\"https://sites.google.com/view/fable-2024\">[개인정보처리방침]</link></color> 에 동의하시나요?";
                break;

            case PopupType.ServerInspection:
                titleTMP.text = "서버 점검";
                descriptTMP.text = "서버가 점검 중이에요. 자세한 내용은 네이버 카페를 통해 확인해주세요.";
                break;

            case PopupType.EqualIDorFailPassword:
                titleTMP.text = "로그인 오류";
                descriptTMP.text = "동일한 아이디가 있거나, 비밀번호를 틀렸어요.";
                break;

            case PopupType.NotID:
                titleTMP.text = "로그인 오류";
                descriptTMP.text = "존재하지 않는 아이디에요.";
                break;

            case PopupType.NoUpdate:
                titleTMP.text = "업데이트 안내";
                descriptTMP.text = "스토어에서 최신 버전을 업데이트 해주세요.";
                break;
        }
    }

    public void ClickButton()
    {
        if (startPanel == null)
            startPanel = FindObjectOfType<StartPanelSystem>();

        startPanel.ClosePanel(startPanel.popupPanel);
        noTouchZone.gameObject.SetActive(false);

        if (popupType == PopupType.NoUpdate || popupType == PopupType.ServerInspection)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        if (popupType == PopupType.InformationConsent)
        {
            startPanel.OpenPanel(startPanel.namePanel);
        }
    }
}
