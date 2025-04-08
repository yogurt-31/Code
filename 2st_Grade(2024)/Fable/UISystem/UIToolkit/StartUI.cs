using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUI : MonoBehaviour
{
    public static StartUI Instance;

    private UIDocument _uiDocument;

    private VisualElement root;

    private VisualElement accountPanel;
    private TextField nicknameField;
    private TextField passwordField;
    private Button createButton;

    private TextElement blinkText;
    private VisualElement fadeImage;
    private VisualElement clickElement;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        root = _uiDocument.rootVisualElement;
        accountPanel = root.Q<VisualElement>("AccountPanel");
        nicknameField = accountPanel.Q<TextField>("NicknameField");
        passwordField = accountPanel.Q<TextField>("PasswordField");
        createButton = accountPanel.Q<Button>("CreateBtn");
        blinkText = root.Q<TextElement>("BlinkTxt");
        fadeImage = root.Q<VisualElement>("FadeImg");
        clickElement = root.Q<VisualElement>("ClickElement");

        if (Information.Instance.GameData.Nickname == "")
            OpenAccountPanel();
        else
        {
            accountPanel.AddToClassList("off");
            accountPanel.pickingMode = PickingMode.Ignore;
            clickElement.RegisterCallback<ClickEvent>(GameStart);
        }

        TextBlink();
    }

    private void GameStart(ClickEvent evt)
    {
        clickElement.UnregisterCallback<ClickEvent>(GameStart);
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => (float)fadeImage.style.opacity.value, x => fadeImage.style.opacity = new StyleFloat(x), 1f, 1f));
        seq.AppendCallback(() => SceneManager.LoadScene("Lobby"));
    }

    private void TextBlink()
    {
        blinkText.style.opacity = 1f;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => (float)blinkText.style.opacity.value, x => blinkText.style.opacity = new StyleFloat(x), 0.3f, 1f));
        seq.AppendInterval(0.2f);
        seq.Append(DOTween.To(() => (float)blinkText.style.opacity.value, x => blinkText.style.opacity = new StyleFloat(x), 1f, 1f));
        seq.AppendInterval(0.2f);
        seq.AppendCallback(() => TextBlink());
    }

    private void OpenAccountPanel()
    {
        PrintErrorCode(string.Empty);

        accountPanel.style.display = DisplayStyle.Flex;
        DOTween.To(() => (float)accountPanel.style.opacity.value, x => accountPanel.style.opacity = new StyleFloat(x), 1f, 0.5f);

        createButton.clicked += MING;
    }

    private void MING()
    {
        PrintErrorCode("계정 생성중...");
        CreateAccount();
    }

    private void CreateAccount()
    {
        if (nicknameField.value == "" || passwordField.value == "") return;

        //if (BackEndManager.Instance.UserSignUp(nicknameField.value, passwordField.value))
        //{
        //    ClosePanel();
        //}
        clickElement.RegisterCallback<ClickEvent>(GameStart);
    }

    public void ClosePanel()
    {
        //accountPanel.style.display = DisplayStyle.None;
        accountPanel.AddToClassList("off");
        PrintErrorCode("ClosePanel이 실행되었습니다.");
    }

    public void PrintErrorCode(string str)
    {
        accountPanel.Q<TextElement>("ErrorMsg").style.display = DisplayStyle.Flex;
        accountPanel.Q<TextElement>("ErrorMsg").style.visibility = Visibility.Visible;
        accountPanel.Q<TextElement>("ErrorMsg").text = str;
    }
}
