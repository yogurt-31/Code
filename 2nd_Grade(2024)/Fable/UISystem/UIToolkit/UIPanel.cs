using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPanel : MonoBehaviour
{
    private Tween tween;
    private Tween noTouchTween;
    
    public bool isPanel { get; set; } = false;
    public bool isAnimating { get; set; } = false;

    public virtual void OpenPanel(VisualElement panel)
    {
        isAnimating = true;

        VisualElement noTouchZone = MainUI.Instance.noTouchZone;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            noTouchZone.style.display = DisplayStyle.Flex;
            panel.style.display = DisplayStyle.Flex;

            noTouchTween?.Kill();
            noTouchTween = FadeElement(noTouchZone, 0f, 1f);
            tween?.Kill();
            tween = FadeElement(panel, 0f, 1f);
        });
        seq.AppendInterval(0.5f);
        seq.OnComplete(() =>
        {
            MainUI.Instance.noTouchZone.RegisterCallback<ClickEvent>((evt) => ClosePanel(panel));
            isPanel = true;
            isAnimating = false;
        });
    }

    public virtual void ClosePanel(VisualElement panel)
    {
        if (isAnimating || !isPanel) return; // Prevent re-entrant calls

        isPanel = false;
        isAnimating = true;

        VisualElement noTouchZone = MainUI.Instance.noTouchZone;
        noTouchZone.UnregisterCallback<ClickEvent>((evt) => ClosePanel(panel));

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            tween?.Kill();
            noTouchTween?.Kill();

            noTouchTween = FadeElement(noTouchZone, 1f, 0f);
            tween = FadeElement(panel, 1f, 0f);

        });
        seq.AppendInterval(0.5f);
        seq.OnComplete(() =>
        {
            panel.style.display = DisplayStyle.None;
            noTouchZone.style.display = DisplayStyle.None;

            LobbyManager.Instance.SelectType = SelectType.NoSelect;

            isAnimating = false;
        });
    }

    private Tween FadeElement(VisualElement uiPanel, float startValue, float endValue)
    {
        uiPanel.style.opacity = new StyleFloat(startValue);
        return DOTween.To(() => (float)uiPanel.style.opacity.value, x => uiPanel.style.opacity = new StyleFloat(x), endValue, 0.5f);
    }
}
