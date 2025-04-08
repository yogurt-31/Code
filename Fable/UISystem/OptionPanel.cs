using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : PanelSystem
{
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private Sprite pressButtonSprite;
    private Image optionPanel;
    private Image noTouchZonePanel;
    private Image frame60Button;
    private Image frame120Button;
    private TextMeshProUGUI offsetNumberText;
    private bool isOptionPanel = false;

    private void Awake()
    {
        noTouchZonePanel = transform.parent.Find("NoTouchZone").GetComponent<Image>();
        optionPanel = transform.Find("OptionPanel").GetComponent<Image>();

        Transform frameTrm = optionPanel.transform.Find("FrameCount");

        frame60Button = frameTrm.Find("60Frame").GetComponent<Image>();
        frame120Button = frameTrm.Find("120Frame").GetComponent<Image>();
        offsetNumberText = optionPanel.transform.Find("Offset").Find("OffsetNumberText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Offset"))
        {
            PlayerPrefs.SetInt("Offset", 0);
        }
        if (!PlayerPrefs.HasKey("Frame"))
        {
            PlayerPrefs.SetInt("Frame", 120);
        }
        Information.Instance.offset = PlayerPrefs.GetInt("Offset");
        Information.Instance.InGameFrame = PlayerPrefs.GetInt("Frame");

        if (Information.Instance.InGameFrame == 60)
        {
            Frame60();
        }
        else
        {
            Frame120();
        }
    }

    public void SettingOffset(int value)
    {
        Information.Instance.offset += value;
        Information.Instance.offset = Mathf.Clamp(Information.Instance.offset, -200, 200);
        offsetNumberText.text = Information.Instance.offset.ToString();
        PlayerPrefs.SetInt("Offset", Information.Instance.offset);
    }

    public void OptionBtn()
    {
        if (!isOptionPanel)
        {
            isOptionPanel = true;
            OpenPanel(noTouchZonePanel);
        }
    }

    public override void OpenPanel(Image noTouchZonePanel)
    {
        base.OpenPanel(noTouchZonePanel);
        offsetNumberText.text = PlayerPrefs.GetInt("Offset").ToString();
    }

    public override void ClosePanel(Image noTouchZonePanel)
    {
        base.ClosePanel(noTouchZonePanel);
        isOptionPanel = false;
    }

    public void Frame60()
    {
        ChangeFrameButton(true);
        Information.Instance.InGameFrame = 60;
        PlayerPrefs.SetInt("Frame", 60);
    }

    public void Frame120()
    {
        ChangeFrameButton(false);
        Information.Instance.InGameFrame = 120;
        PlayerPrefs.SetInt("Frame", 120);
    }

    public void ChangeFrameButton(bool is60Frame)
    {
        Image pressButton = null;
        Image unpressButton = null;
        if(is60Frame)
        {
            pressButton = frame60Button;
            unpressButton = frame120Button;
        }
        else
        {
            pressButton = frame120Button;
            unpressButton = frame60Button;
        }
        pressButton.sprite = pressButtonSprite;
        unpressButton.sprite = buttonSprite;

        pressButton.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition = PressPosition();
        unpressButton.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition = BtnPosition();
    }

    private Vector3 PressPosition() => new Vector3(0, -5, 0);
    private Vector3 BtnPosition() => new Vector3(0, 15, 0);

    public void ExitGame()
    {
        Application.Quit();
    }
}
