using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private Transform background;
    private Transform shopTypePanel;
    private CanvasGroup canvasGroup;
    private GameObject[] shopPanels = new GameObject[4];

    private Transform topPanel;
    private TextMeshProUGUI ticketText;
    private TextMeshProUGUI coinText;

    private int activeShopTypeNum = 0;

    private void Awake()
    {
        background = transform.parent.Find("Background");
        shopTypePanel = transform.Find("ShopTypePanel");
        canvasGroup = GetComponent<CanvasGroup>();
        for (int i = 0; i < shopTypePanel.childCount; ++i) shopPanels[i] = shopTypePanel.GetChild(i).gameObject;
        topPanel = transform.Find("TopPanel");
        ticketText = topPanel.Find("TicketPanel").GetChild(0).GetComponent<TextMeshProUGUI>();
        coinText = topPanel.Find("CoinPanel").GetChild(0).GetComponent<TextMeshProUGUI>();
        ChangeCoinAndTicket();
    }

    public void SetActiveShopPanel(bool isTrue)
    {
        Sequence seq = DOTween.Sequence();
        if(isTrue)
        {
            seq.Append(background.DOScaleX(1, 0.3f));
            seq.Append(canvasGroup.DOFade(1, 0.5f));
        }
        else
        {
            seq.Append(canvasGroup.DOFade(0, 0.5f));
            seq.Append(background.DOScaleX(0, 0.3f));
        }
        canvasGroup.interactable = isTrue;
        canvasGroup.blocksRaycasts = isTrue;
    }

    public void SelectShopType(int shopNum)
    {
        ChangeShopPanel(shopNum);
    }

    private void ChangeShopPanel(int shopNum)
    {
        shopPanels[activeShopTypeNum].SetActive(false);
        shopPanels[shopNum].SetActive(true);
        activeShopTypeNum = shopNum;
    }

    public void ChangeCoinAndTicket()
    {
        ticketText.text = Information.Instance.GameData.EnterTicket.ToString();
        coinText.text = Information.Instance.GameData.Coin.ToString();
    }
}
