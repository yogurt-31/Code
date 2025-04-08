using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : PanelSystem
{
    private Image noTouchZonePanel;

    private bool isExitPanel;
    private void Awake()
    {
        noTouchZonePanel = transform.parent.Find("NoTouchZone").GetComponent<Image>();
    }

    #region Panel Open

    public void PanelOpen()
    {
        OpenPanel(noTouchZonePanel);
    }

    public override void OpenPanel(Image noTouchZonePanel)
    {
        base.OpenPanel(noTouchZonePanel);
        isExitPanel = true;
    }

    #endregion

    #region Panel Close

    public void PanelClose()
    {
        ClosePanel(noTouchZonePanel);
    }
    public override void ClosePanel(Image noTouchZonePanel)
    {
        base.ClosePanel(noTouchZonePanel);
        isExitPanel = false;
    }

    #endregion
}
