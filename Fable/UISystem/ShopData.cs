using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopDataType
{
    Non_Image,
    Image,
}
public class ShopData : MonoBehaviour
{
    public ShopDataType shopDataType;

    [SerializeField] private ShopDataSO dataSO;
    private Image dataImage;
    private TextMeshProUGUI dataNameText;

    public void TouchItem()
    {
        BuyPanel.instance.GetShopData(dataSO);
    }

    private void Awake()
    {
        if (shopDataType == ShopDataType.Image)
        {
            dataImage = transform.Find("DataImage").GetComponent<Image>();
            dataNameText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();

            dataImage.sprite = dataSO.dataImage;
        }
        else dataNameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dataNameText.text = dataSO.dataName;
    }
}
