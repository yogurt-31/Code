using TMPro;
using UnityEngine.UI;

public class BuyPanel : PanelSystem
{
    public static BuyPanel instance;

    private Button buyButton;

    private TextMeshProUGUI dataNameText;
    private TextMeshProUGUI dataPriceText;
    private TextMeshProUGUI noMoneyText;

    private Image noTouchZonePanel;
    private ShopDataSO shopData;
    private ShopPanel shopPanel;
    private void Awake()
    {
        if (instance == null) instance = this;
        noTouchZonePanel = transform.parent.Find("NoTouchZone").GetComponent<Image>();
        shopPanel = transform.parent.GetComponentInParent<ShopPanel>();
        buyButton = transform.Find("BuyButton").GetComponent<Button>();
        buyButton.onClick.AddListener(BuyItemButton);
        dataNameText = transform.Find("BuyNameImage").GetComponentInChildren<TextMeshProUGUI>();
        dataPriceText = transform.Find("PriceImage").GetComponentInChildren<TextMeshProUGUI>();
        noMoneyText = transform.Find("NoMoneyText").GetComponent<TextMeshProUGUI>();
        noMoneyText.enabled = false;
    }

    public void GetShopData(ShopDataSO shopData)
    {
        this.shopData = shopData;
        dataNameText.text = shopData.dataName;
        dataPriceText.text = shopData.dataPrice.ToString();

        bool canBuy = Information.Instance.GameData.Coin - shopData.dataPrice >= 0;
        noMoneyText.enabled = !canBuy;
        buyButton.interactable = canBuy;

        OpenPanel(noTouchZonePanel);
    }
    public override void OpenPanel(Image noTouchZonePanel)
    {
        base.OpenPanel(noTouchZonePanel);
    }

    private void BuyItemButton()
    {
        Information.Instance.GameData.Coin -= shopData.dataPrice;
        shopPanel.ChangeCoinAndTicket();
        ClosePanel(noTouchZonePanel);
    }
    public override void ClosePanel(Image noTouchZonePanel)
    {
        base.ClosePanel(noTouchZonePanel);
    }
}
