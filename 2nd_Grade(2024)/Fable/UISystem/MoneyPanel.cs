using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    private TextMeshProUGUI ticketText;

    private void Awake()
    {
        coinText = transform.Find("CoinCountText").GetComponent<TextMeshProUGUI>();
        ticketText = transform.Find("TicketCountText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        coinText.text = Information.Instance.GameData.Coin.ToString();
        ticketText.text = Information.Instance.GameData.EnterTicket.ToString();
    }
}
