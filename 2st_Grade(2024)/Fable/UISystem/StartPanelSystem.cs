using DG.Tweening;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanelSystem : MonoBehaviour, IPointerClickHandler
{
    public Image namePanel;
    public Image popupPanel;
    [SerializeField] private Image noTouchZone;
    [SerializeField] private Image fadeImage;

    [SerializeField] private TMP_InputField nickNameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [SerializeField] private TextMeshProUGUI versionTMP;

    private PopupPanel PopupPanel;

    private bool isStart = false;
    private string nickName;
    private string password;

    private Sequence seq;

    private void Start()
    {
        PopupPanel = FindObjectOfType<PopupPanel>();
        nickNameInputField.onValueChanged.AddListener((word) => nickNameInputField.text = Regex.Replace(word, @"[^0-9a-zA-Z°¡-ÆR]", ""));
        passwordInputField.onValueChanged.AddListener((word) => passwordInputField.text = Regex.Replace(word, @"[<>]", ""));
        versionTMP.text = "V " + Application.version;
        if (Information.Instance.GameData.Nickname != "")
        {
            noTouchZone.gameObject.SetActive(false);
            BackEndManager.Instance.UserSignIn(Information.Instance.GameData.Nickname, Information.Instance.GameData.PassWord);
            return;
        }
        PopupPanel.SettingPanel(PopupType.InformationConsent);
        OpenPanel(popupPanel);
    }
    public void NicknameChange(string str)
    {
        nickName = str;
    }

    public void PasswordChange(string str)
    {
        password = str;
    }

    public void Check()
    {

    }

    public void AccountCheck()
    {
        if (nickName == "" || password == "") return;
        BackEndManager.Instance.UserSignUp(nickName, password);
    }

    #region Panel ON/OFF

    public void OpenPanel(Image panel)
    {
        DOTween.Kill(noTouchZone);
        seq = DOTween.Sequence();
        seq.Append(panel.rectTransform.DOScale(1f, 0.5f));
        seq.JoinCallback(() => noTouchZone.gameObject.SetActive(true));
        seq.Join(noTouchZone.DOFade(0.3f, 0.5f));
    }

    public void ClosePanel(Image panel)
    {
        seq = DOTween.Sequence();
        seq.AppendCallback(() => panel.rectTransform.DOScale(0f, 0.5f));
        seq.JoinCallback(() =>
        {
            if (PopupPanel.popupType != PopupType.InformationConsent)
                noTouchZone.DOFade(0f, 0.5f);
        });
        seq.OnComplete(()=> noTouchZone.gameObject.SetActive(false));
    }

    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Information.Instance.GameData.Nickname == "" || Information.Instance.GameData.PassWord == "")
        {
            OpenPanel(namePanel);
            return;
        }

        if (isStart) return;
        isStart = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(fadeImage.DOFade(1f, 1f));
        seq.OnComplete(() => SceneManager.LoadScene("Lobby"));
    }
}