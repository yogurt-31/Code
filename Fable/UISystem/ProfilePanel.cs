using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : PanelSystem
{
    [SerializeField] private AnimationCurve xpGaugeAnimationCurve;
    [SerializeField] private ThemeTypeSO[] themeTypeSO;
    [SerializeField] private Image moneyPanel;
    private GameData gameData;
    private SaveLoadData saveLoadData;

    #region 프로필 패널

    private Button profileBtn;
    
    private Transform profilePanel;

    private Image noTouchZonePanel;
    private Image xpGaugeImage;
    private Image emptyXpGaugeImage;

    private TextMeshProUGUI nickNameText;
    private TextMeshProUGUI levelNumberText;
    private TextMeshProUGUI xpNumberText;
      
    private TextMeshProUGUI profileText;
    private TextMeshProUGUI themeTagText;
    private TextMeshProUGUI levelText;


    #endregion

    #region 테마 패널

    private Button themeBtn;
    private Button themeReturnBtn;

    private Transform themeSelectPanel;

    private TextMeshProUGUI themeText;
    private TextMeshProUGUI returnThemeText;

    #endregion

    #region 칭호 패널

    private Button tagBtn;
    private Button tagReturnBtn;
    private Button[] tagSelectBtns;

    private Transform tagSelectPanel;

    private TextMeshProUGUI tagText;
    private TextMeshProUGUI returnTagText;
    private TextMeshProUGUI[] tagSelectTexts = new TextMeshProUGUI[10];

    #endregion

    private bool isProfilePanel = false;
    private void Awake()
    {
        Initialize();

        themeBtn.onClick.AddListener(() => ChangeThemePanel(true));
        themeReturnBtn.onClick.AddListener(() => ChangeThemePanel(false));

        tagBtn.onClick.AddListener(() => ChangeTagPanel(true));
        tagReturnBtn.onClick.AddListener(() => ChangeTagPanel(false));

    }

    private void Start()
    {
        saveLoadData = Information.Instance.GetComponent<SaveLoadData>();
        gameData = Information.Instance.GameData;
        ChangeTheme(Information.Instance.ThemeDictionary[gameData.selectedTheme]);
        SelectTagButton((int)gameData.selectedAchieve);
    }

    private void Initialize()
    {
        profilePanel = transform.Find("ProfilePanel");
        themeSelectPanel = transform.Find("ThemeSelectPanel");
        tagSelectPanel = transform.Find("TagSelectPanel");

        profileBtn = moneyPanel.transform.Find("ProfileButton").GetComponent<Button>();

        noTouchZonePanel = transform.parent.Find("NoTouchZone").GetComponent<Image>();

        themeBtn = transform.Find("ThemeButton").GetComponent<Button>();
        tagBtn = transform.Find("TagButton").GetComponent<Button>();

        profileText = profilePanel.Find("ProfileText").GetComponent<TextMeshProUGUI>();
        themeTagText = profilePanel.Find("TagText").GetComponent<TextMeshProUGUI>();
        nickNameText = profilePanel.Find("NicknameText").GetComponent<TextMeshProUGUI>();

        xpGaugeImage = profilePanel.Find("XPGauge").GetComponent<Image>();
        emptyXpGaugeImage = xpGaugeImage.transform.Find("EmptyXPGauge").GetComponent<Image>();

        levelNumberText = xpGaugeImage.transform.Find("LevelNumberText").GetComponent<TextMeshProUGUI>();
        levelText = xpGaugeImage.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        xpNumberText = xpGaugeImage.transform.Find("XPNumberText").GetComponent<TextMeshProUGUI>();

        themeText = themeSelectPanel.Find("ThemeText").GetComponent<TextMeshProUGUI>();
        themeReturnBtn = themeSelectPanel.Find("ReturnButton").GetComponent<Button>();
        returnThemeText = themeReturnBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        tagText = tagSelectPanel.Find("TagText").GetComponent<TextMeshProUGUI>();
        tagReturnBtn = tagSelectPanel.Find("ReturnButton").GetComponent<Button>();
        returnTagText = tagReturnBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tagSelectBtns = tagSelectPanel.Find("LayoutGroup").GetComponentsInChildren<Button>();
        for (int i = 0; i < tagSelectBtns.Length; ++i) tagSelectTexts[i] = tagSelectBtns[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    #region 테마 관련 함수

    private void ChangeThemePanel(bool isTrue)
    {
        themeSelectPanel.gameObject.SetActive(isTrue);
        if (isTrue)
            tagSelectPanel.gameObject.SetActive(!isTrue);
    }

    public void SelectThemeTypeButton(int themeTypeNum)
    {
        gameData.selectedTheme = (ThemeType)themeTypeNum;
        ChangeTheme(Information.Instance.ThemeDictionary[gameData.selectedTheme]);
    }

    public void ChangeTheme(ThemeTypeSO themeType)
    {
        Color themeTypeColor = themeType.themeColor;

        moneyPanel.sprite = themeType.goodsImage;
        profileBtn.image.sprite = themeType.profileIcon;

        profilePanel.GetComponent<Image>().sprite = themeType.themeImage;
        themeSelectPanel.GetComponent<Image>().sprite = themeType.themeImage;
        tagSelectPanel.GetComponent<Image>().sprite = themeType.themeImage;

        xpGaugeImage.sprite = themeType.xpGaugeImage;
        emptyXpGaugeImage.sprite = themeType.xpGaugeImage;

        themeReturnBtn.image.sprite = themeType.btnImage;
        tagReturnBtn.image.sprite = themeType.btnImage;

        profileText.color = themeTypeColor;
        themeTagText.color = themeTypeColor;
        nickNameText.color = themeTypeColor;
        levelText.color = themeTypeColor;
        levelNumberText.color = themeTypeColor;
        xpNumberText.color = themeTypeColor;
        themeText.color = themeTypeColor;
        returnThemeText.color = themeTypeColor;

        tagText.color = themeTypeColor;
        for(int i = 0; i < tagSelectBtns.Length; ++i)
        {
            tagSelectTexts[i].color = themeTypeColor;
            tagSelectBtns[i].image.sprite = themeType.btnImage;
        }
        returnTagText.color = themeTypeColor;
    }

    #endregion

    #region 칭호 관련 함수

    private void ChangeTagPanel(bool isTrue)
    {
        tagSelectPanel.gameObject.SetActive(isTrue);
        if (isTrue)
            themeSelectPanel.gameObject.SetActive(!isTrue);
    }

    public void SelectTagButton(int themeTypeNum)
    {
        gameData.selectedAchieve = (AchieveType)themeTypeNum;
        themeTagText.text = Information.Instance.AchieveDicionary[gameData.selectedAchieve];
    }

    #endregion

    private void OnEnable()
    {
        //xpButtonGaugeImage.fillAmount = gameData.Exp * 0.001f;
        noTouchZonePanel.gameObject.SetActive(false);
    }
    public void XPGaugeBtn()
    {
        if (!isProfilePanel)
        {
            isProfilePanel = true;
            OpenPanel(noTouchZonePanel);
        }
    }

    public override void OpenPanel(Image noTouchZonePanel)
    {
        
        profilePanel.gameObject.SetActive(true);
        noTouchZonePanel.gameObject.SetActive(true);

        nickNameText.text = gameData.Nickname;
        levelNumberText.text = gameData.LV.ToString();

        xpNumberText.text = "0/1000";

        xpGaugeImage.fillAmount = 0;

        Sequence seq = DOTween.Sequence();
        seq.Append(noTouchZonePanel.DOFade(0.3f, 0.5f));
        seq.Join(transform.DOScale(1f, 0.5f));
        seq.OnComplete(() =>
        {
            noTouchZonePanel.GetComponent<NoTouchZone>().enabled = true;
            StartCoroutine(FillCoroutine(0.7f));
        });
        seq.Join(themeBtn.GetComponent<CanvasGroup>().DOFade(1f, 0.2f));
        seq.Join(tagBtn.GetComponent<CanvasGroup>().DOFade(1f, 0.2f));

    }

    public override void ClosePanel(Image noTouchZonePanel)
    {
        noTouchZonePanel.GetComponent<NoTouchZone>().enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0f, 0.5f));
        seq.Join(themeBtn.GetComponent<CanvasGroup>().DOFade(0f, 0.2f));
        seq.Join(tagBtn.GetComponent<CanvasGroup>().DOFade(0f, 0.2f));
        seq.Join(noTouchZonePanel.DOFade(0f, 0.5f));
        seq.OnComplete(() =>
        {
            noTouchZonePanel.gameObject.SetActive(false);
            profilePanel.gameObject.SetActive(false);
            themeSelectPanel.gameObject.SetActive(false);
            tagSelectPanel.gameObject.SetActive(false);
            isProfilePanel = false;
        });
    }

    private IEnumerator FillCoroutine(float time)
    {
        float currentTime = 0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            float xpGauge = Mathf.Lerp(0f, gameData.Exp, xpGaugeAnimationCurve.Evaluate(currentTime / time));
            xpGaugeImage.fillAmount = xpGauge * 0.001f;
            xpNumberText.text = (int)(xpGauge) + "/1000";
            yield return null;
        }
        yield return null;
    }
}
