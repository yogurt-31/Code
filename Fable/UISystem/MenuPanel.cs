using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    private Image menuPanel;
    private bool isMenuPanel;

    private void Awake()
    {
        menuPanel = transform.Find("MenuPanel").GetComponent<Image>();
    }

    public void MenuButton()
    {
        isMenuPanel = !isMenuPanel;
        menuPanel.rectTransform.DOAnchorPosY(isMenuPanel ? 350 : 950, 0.3f);
    }
}
