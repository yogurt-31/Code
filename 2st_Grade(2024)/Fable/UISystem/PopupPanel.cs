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
                titleTMP.text = "��������ó����ħ";
                descriptTMP.text = "<color=#AAAAFF><link=\"https://sites.google.com/view/fable-2024\">[��������ó����ħ]</link></color> �� �����Ͻó���?";
                break;

            case PopupType.ServerInspection:
                titleTMP.text = "���� ����";
                descriptTMP.text = "������ ���� ���̿���. �ڼ��� ������ ���̹� ī�並 ���� Ȯ�����ּ���.";
                break;

            case PopupType.EqualIDorFailPassword:
                titleTMP.text = "�α��� ����";
                descriptTMP.text = "������ ���̵� �ְų�, ��й�ȣ�� Ʋ�Ⱦ��.";
                break;

            case PopupType.NotID:
                titleTMP.text = "�α��� ����";
                descriptTMP.text = "�������� �ʴ� ���̵𿡿�.";
                break;

            case PopupType.NoUpdate:
                titleTMP.text = "������Ʈ �ȳ�";
                descriptTMP.text = "������ �ֽ� ������ ������Ʈ ���ּ���.";
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
