using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class PanelSystem : MonoBehaviour
{
    public virtual void OpenPanel(Image noTouchZonePanel)
    {
        gameObject.SetActive(true);
        noTouchZonePanel.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        seq.Append(noTouchZonePanel.DOFade(0.3f, 0.5f));
        seq.Join(transform.DOScale(1f, 0.5f));
        seq.OnComplete(() =>
        {
            noTouchZonePanel.GetComponent<NoTouchZone>().enabled = true;
        });
    }

    public virtual void ClosePanel(Image noTouchZonePanel)
    {
        noTouchZonePanel.GetComponent<NoTouchZone>().enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0f, 0.5f));
        seq.Join(noTouchZonePanel.DOFade(0f, 0.5f));
        seq.OnComplete(() =>
        {
            noTouchZonePanel.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }
}
