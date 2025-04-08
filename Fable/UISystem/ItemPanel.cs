using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private List<Image> BlessCheckImages;
    [SerializeField] private List<int> BlessPrice;
    [SerializeField] private TextMeshProUGUI currentCoinText;
    [SerializeField] private TextMeshProUGUI useCoinText;
    [SerializeField] private TextMeshProUGUI remainCoinText;

    public bool isCanPay;

    private void Start()
    {
        for(int i = 0; i < BlessCheckImages.Count; i++)
        {
            BlessCheckImages[i].enabled = Information.Instance.GameData.IsBlessOn[i];
        }

        CheckCanPay();
    }

    public void SelectItem(int itemNum)
    {
        Information.Instance.GameData.IsBlessOn[itemNum] = !Information.Instance.GameData.IsBlessOn[itemNum];
        BlessCheckImages[itemNum].enabled = Information.Instance.GameData.IsBlessOn[itemNum];
        CheckCanPay();
    }

    private void CheckCanPay()
    {
        currentCoinText.text = Information.Instance.GameData.Coin.ToString();
        int useGold = 0;
        for(int i = 0; i < Information.Instance.GameData.IsBlessOn.Length; i++)
        {
            if (Information.Instance.GameData.IsBlessOn[i])
            {
                useGold += BlessPrice[i];
            }

            useCoinText.text = useGold.ToString();
            if(useGold > Information.Instance.GameData.Coin)
            {
                isCanPay = false;
            }
            else
            {
                isCanPay = true;
            }

            int remainCoin = Information.Instance.GameData.Coin - useGold;
            remainCoinText.text = remainCoin.ToString();
        }
    }
}