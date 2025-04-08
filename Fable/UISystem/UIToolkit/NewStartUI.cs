using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewStartUI : MonoBehaviour
{
    private VisualElement root;
    /*private VisualElement accountPanel;
    private TextField passwordField;

    private Button createButton;*/

    //private VisualElement clickElement;

    private void Start()
    {
        TextBlink();
    }
    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();

        root = document.rootVisualElement;
        /*clickElement = root.Q<VisualElement>("ClickElement");
        clickElement.pickingMode = PickingMode.Position;*/

        /*accountPanel = root.Q<VisualElement>("AccountPanel");
        nicknameField = accountPanel.Q<TextField>("NicknameField");
        passwordField = accountPanel.Q<TextField>("PasswordField");
        createButton = accountPanel.Q<Button>("CreateBtn");*/


        /*if (Information.Instance.GameData.Nickname != "" && Information.Instance.GameData.PassWord != "")
        {
            //if (BackEndManager.Instance.UserSignIn(Information.Instance.GameData.Nickname, Information.Instance.GameData.PassWord))
            //    ClosePanel();
        }
        else
        {
            if(accountPanel.ClassListContains("off"))
                accountPanel.RemoveFromClassList("off");


            Debug.Log(createButton.pickingMode);

            createButton.clicked += delegate
            {
                if (nicknameField.value != string.Empty || passwordField.value != string.Empty)
                {
                    //if (BackEndManager.Instance.UserSignUp(nicknameField.value, passwordField.value))
                    //    ClosePanel();
                } 
            };
        }*/
        //createButton.RegisterCallback<PointerDownEvent>(evt =>
        //{
        //    Debug.Log("asdf");
        //    if (nicknameField.value != string.Empty || passwordField.value != string.Empty)
        //    {
        //        if (BackEndManager.Instance.UserSignUp(nicknameField.value, passwordField.value))
        //        {
        //            ClosePanel();
        //        }
        //    }
        //});


    }

    /*public void ClosePanel()
    {
        if(!accountPanel.ClassListContains("off"))
            accountPanel.AddToClassList("off");
        clickElement.pickingMode = PickingMode.Position;
        clickElement.RegisterCallback<ClickEvent>(evt =>
        {
            clickElement.pickingMode = PickingMode.Ignore;
            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => (float)clickElement.style.opacity.value, x => clickElement.style.opacity = new StyleFloat(x), 1f, 1f));
            seq.AppendCallback(() => SceneManager.LoadScene("Lobby"));
        });
    }*/

    private void TextBlink()
    {
        TextElement blinkText = root.Q<TextElement>("BlinkTxt");
        
        if(Information.Instance.IsKorean)
        {
            blinkText.text = "화면을 터치해 주세요!";
        }
        else
        {
            blinkText.text = "Touch to Screen!";
        }

        blinkText.style.opacity = 1f;
        Sequence seq = DOTween.Sequence();
        seq.Append(DOTween.To(() => (float)blinkText.style.opacity.value, x => blinkText.style.opacity = new StyleFloat(x), 0.3f, 1f));
        seq.AppendInterval(0.2f);
        seq.Append(DOTween.To(() => (float)blinkText.style.opacity.value, x => blinkText.style.opacity = new StyleFloat(x), 1f, 1f));
        seq.AppendInterval(0.2f);
        seq.AppendCallback(() => TextBlink());
    }
}
