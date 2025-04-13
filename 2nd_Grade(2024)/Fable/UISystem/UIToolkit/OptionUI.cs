using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionUI : UIPanel
{
    private VisualElement optionPanel;

    private Button vibrationButton;
    private Button fpsButton;
    private Button effectButton;
    private Button turnOffButton;

    private TextElement vibrationText;
    private TextElement fpsText;
    private TextElement effectText;
    private TextElement turnOffText;

    Tween tween;

    private void OnEnable()
    {
        optionPanel = MainUI.Instance.optionPanel;

        VisualElement gameOptionPanel = optionPanel.Q<VisualElement>("GameOptionPanel");

        VisualElement vibrationElement = gameOptionPanel.Q<VisualElement>("VibrationElement");
        VisualElement fpsElement = gameOptionPanel.Q<VisualElement>("FPSElement");
        VisualElement effectElement = gameOptionPanel.Q<VisualElement>("EffectElement");
        VisualElement turnOffElement = gameOptionPanel.Q<VisualElement>("TurnOffElement");

        vibrationText = vibrationElement.Q<TextElement>("JindongTxt");
        fpsText = fpsElement.Q<TextElement>("FPSTxt");
        effectText = effectElement.Q<TextElement>("EffectTxt");
        turnOffText = turnOffElement.Q<TextElement>("TurnOffTxt");

        if(Information.Instance.IsKorean)
        {
            vibrationText.text = "진동";
            fpsText.text = "저사양 모드";
            effectText.text = "이펙트";
            turnOffText.text = "게임 종료";
        }
        else
        {
            vibrationText.text = "Vibration";
            fpsText.text = "FPS Down";
            effectText.text = "Touch Effect";
            turnOffText.text = "Quit";
        }

        vibrationButton = vibrationElement.Q<Button>("ONOFFBtn");
        vibrationButton.clicked += HandleVibrationClick;
        SwitchButton(vibrationButton, Information.Instance.IsShake);

        fpsButton = fpsElement.Q<Button>("ONOFFBtn");
        fpsButton.clicked += HandleFPSClick;
        SwitchButton(fpsButton, Information.Instance.InGameFrame == 120 ? false : true);

        effectButton = effectElement.Q<Button>("ONOFFBtn");
        effectButton.clicked += HandleEffectClick;
        SwitchButton(effectButton, Information.Instance.ShowEffect);

        turnOffButton = turnOffElement.Q<Button>("TurnOffBtn");

        if (Information.Instance.IsKorean)
            turnOffButton.text = "게임 종료";
        else
            turnOffButton.text = "Quit";

        turnOffButton.clicked += HandleTurnOffGame;
    }

    private void HandleTurnOffGame()
    {
        Application.Quit();
    }

    private void HandleVibrationClick()
    {
        if (isAnimating) return; // 애니메이션 중에는 버튼 클릭 방지
        Information.Instance.IsShake = !Information.Instance.IsShake;
        SwitchButton(vibrationButton, Information.Instance.IsShake);
    }

    private void HandleFPSClick()
    {
        if (isAnimating) return; // 애니메이션 중에는 버튼 클릭 방지
        Information.Instance.InGameFrame = Information.Instance.InGameFrame == 120 ? 60 : 120;
        SwitchButton(fpsButton, Information.Instance.InGameFrame == 120 ? false : true);
    }

    private void HandleEffectClick()
    {
        if (isAnimating) return; // 애니메이션 중에는 버튼 클릭 방지
        Information.Instance.ShowEffect = !Information.Instance.ShowEffect;
        SwitchButton(effectButton, Information.Instance.ShowEffect);
    }

    private void SwitchButton(Button toggleButton, bool isTrue)
    {
        if (isTrue)
        {
            toggleButton.AddToClassList("on");
            toggleButton.RemoveFromClassList("off");
            toggleButton.Q<VisualElement>("ButtonSwitch").RemoveFromClassList("off");
            toggleButton.Q<VisualElement>("ButtonSwitch").AddToClassList("on");
        }
        else
        {
            toggleButton.RemoveFromClassList("on");
            toggleButton.Q<VisualElement>("ButtonSwitch").RemoveFromClassList("on");
            toggleButton.Q<VisualElement>("ButtonSwitch").AddToClassList("off");
        }
    }
}
