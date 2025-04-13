using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    private UIDocument uiDocument;

    [SerializeField] private Popup tutorialUI;

    public VisualElement root { get; private set; }
    public VisualElement uiPanel { get; private set; }
    public VisualElement optionPanel { get; private set; }
    public VisualElement profilePanel { get; private set; }
    public VisualElement rankingPanel { get; private set; }
    public VisualElement rhythmPanel { get; private set; }
    public VisualElement exchangePanel { get; private set; }
    public VisualElement TagPanel { get; private set; }
    public VisualElement noTouchZone { get; private set; }

    private VisualElement theme;
    private VisualElement buttonElement;
    private VisualElement fadeImage;
    private VisualElement upArrow;
    private VisualElement downArrow;

    public OptionUI optionUI { get; private set; }
    public ProfileUI profileUI { get; private set; }
    public RankingUI rankingUI { get; private set; }
    public RhythmUI rhythmUI { get; private set; }
    public ExchangeUI exchangeUI { get; private set; }
    public TagUI tagUI { get; private set; }

    public bool isBlinking = true;
    private float blinkDuration = 1f;
    private float timer = 0f;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        optionUI = GetComponent<OptionUI>();
        profileUI = GetComponent<ProfileUI>();
        rankingUI = GetComponent<RankingUI>();
        rhythmUI = GetComponent<RhythmUI>();
        exchangeUI = GetComponent<ExchangeUI>();
        Instance = this;

        if (!Information.Instance.GameData.IsTutorialPopup)
        {
            Information.Instance.GameData.IsTutorialPopup = true;
            tutorialUI.OpenTutorialPanel();
        }
    }
    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;

        theme = root.Q<VisualElement>("Theme");
        buttonElement = root.Q<VisualElement>("ButtonElement");
        uiPanel = root.Q<VisualElement>("UIPanel");
        fadeImage = root.Q<VisualElement>("FadeImg");

        optionPanel = uiPanel.Q<VisualElement>("OptionUI");
        profilePanel = uiPanel.Q<VisualElement>("ProfileUI");
        rankingPanel = uiPanel.Q<VisualElement>("RankingUI");
        rhythmPanel = root.Q<VisualElement>("RhythmUI");
        exchangePanel = uiPanel.Q<VisualElement>("ExchangeUI");
        noTouchZone = root.Q<VisualElement>("NoTouchZone");
        noTouchZone.style.display = DisplayStyle.None;

        upArrow = root.Q<VisualElement>("UpArrow");
        downArrow = root.Q<VisualElement>("DownArrow");

        FadeImage(true);
        MoneyPanelSetting();
        ButtonClick();
    }
    private void Update()
    {
        if (isBlinking)
        {
            timer += Time.deltaTime;
            float value = Mathf.PingPong(timer / blinkDuration, 1);
            upArrow.style.opacity = value;
            downArrow.style.opacity = value;
        }
    }

    public void BlinkArrow()
    {
        if (!isBlinking)
        {
            upArrow.style.opacity = 0;
            downArrow.style.opacity = 0;
        }
        else
        {
            upArrow.style.opacity = 1;
            downArrow.style.opacity = 1;
        }
    }

    public void FadeImage(bool isFadeIn)
    {
        if (!isFadeIn)
        {
            fadeImage.style.opacity = 0f;
            DOTween.To(() => (float)fadeImage.style.opacity.value, x => fadeImage.style.opacity = new StyleFloat(x), 1f, 1f);
        }
        else
        {
            fadeImage.style.opacity = 1f;
            DOTween.To(() => (float)fadeImage.style.opacity.value, x => fadeImage.style.opacity = new StyleFloat(x), 0f, 1f);
        }
    }

    public void MoneyPanelSetting()
    {
        VisualElement coinPanel = theme.Q<VisualElement>("CoinPanel");
        VisualElement ticketPanel = theme.Q<VisualElement>("TicketPanel");

        Label coinText = coinPanel.Q<Label>("CoinTxt");
        Label ticketText = ticketPanel.Q<Label>("TicketTxt");

        coinText.text = Information.Instance.GameData.Coin.ToString();
        ticketText.text = Information.Instance.GameData.EnterTicket.ToString();
    }

    private void ButtonClick()
    {
        Button profileButton = root.Q<Button>("ProfileBtn");
        Button optionButton = buttonElement.Q<Button>("OptionBtn");
        Button rankingButton = buttonElement.Q<Button>("RankingBtn");
        Button tutorialButton = buttonElement.Q<Button>("TutorialBtn");
        Button exchangeButton = theme.Q<Button>("ExchangeBtn");

        profileButton.RegisterCallback<ClickEvent>(evt =>
        {
            if (profileUI.isPanel) return;
            profileUI.OpenPanel(profilePanel);
            LobbyManager.Instance.SelectType = SelectType.Select;
        });
        optionButton.RegisterCallback<ClickEvent>(evt =>
        {
            if (optionUI.isPanel) return;
            optionUI.OpenPanel(optionPanel);
            LobbyManager.Instance.SelectType = SelectType.Select;
        });
        rankingButton.RegisterCallback<ClickEvent>(evt =>
        {
            if (rankingUI.isPanel) return;
            rankingUI.OpenPanel(rankingPanel);
            LobbyManager.Instance.SelectType = SelectType.Select;
        });
        tutorialButton.RegisterCallback<ClickEvent>(evt =>
        {
            tutorialUI.OpenTutorialPanel();
        });
        exchangeButton.RegisterCallback<ClickEvent>(evt =>
        {
            if (exchangeUI.isPanel) return;
            exchangeUI.OpenPanel(exchangePanel);
            LobbyManager.Instance.SelectType = SelectType.Select;
        });
    }
    public void SelectIsland()
    {
        isBlinking = false;
        BlinkArrow();
        rhythmUI.OpenPanel(true);
        LobbyManager.Instance.SelectType = SelectType.Select;
    }
    public void OpenTagUI()
    {

    }
}
