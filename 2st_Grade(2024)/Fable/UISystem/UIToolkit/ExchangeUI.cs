using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ExchangeUI : UIPanel
{
    private VisualElement exchangePanel;
    private TextElement titleText;
    private TextElement ticketCountText;
    private TextElement coinCountText;
    private TextElement noCoinText;

    private Tween tween;

    private int ticketCnt = 1;
    private int coinCnt = 250;

    private void OnEnable()
    {
        exchangePanel = MainUI.Instance.exchangePanel;
        //titleText = exchangePanel.Q<TextElement>("TitleText");
        ticketCountText = exchangePanel.Q<TextElement>("TicketCntTxt");
        coinCountText = exchangePanel.Q<TextElement>("CoinTxt");
        noCoinText = exchangePanel.Q<TextElement>("NoCoinTxt");

        //if (Information.Instance.IsKorean)
        //    titleText.text = "티켓 교환";
        //else
        //    titleText.text = "Exchange Ticket";

        Button plusButton = exchangePanel.Q<Button>("PlusBtn");
        Button minusButton = exchangePanel.Q<Button>("MinusBtn");
        Button exchangeButton = exchangePanel.Q<Button>("ExchangeBtn");

        if (Information.Instance.IsKorean)
            exchangeButton.text = "교환";
        else
            exchangeButton.text = "Exchange";

        plusButton.RegisterCallback<ClickEvent>((evt) => CalculateTicket(1));
        minusButton.RegisterCallback<ClickEvent>((evt) => CalculateTicket(-1));
        exchangeButton.RegisterCallback<ClickEvent>((evt) => BuyTicket());
        ResetSetting();
    }

    private void CalculateTicket(int value)
    {
        if (ticketCnt + value < 1) return;
        ticketCnt += value;
        coinCnt += 250 * value;
        ticketCountText.text = "x" + ticketCnt;
        noCoinText.style.opacity = 0;
        coinCountText.text = coinCnt.ToString();
    }

    private void BuyTicket()
    {
        if(Information.Instance.GameData.Coin < coinCnt)
        {
            noCoinText.style.opacity = 1;
        }
        else
        {
            Information.Instance.GameData.Coin -= coinCnt;
            Information.Instance.GameData.EnterTicket += ticketCnt;
            MainUI.Instance.MoneyPanelSetting();
            ResetSetting();
        }
    }

    private void ResetSetting()
    {
        ticketCnt = 1;
        coinCnt = 250;
        ticketCountText.text = "x" + ticketCnt;
        coinCountText.text = coinCnt.ToString();
        noCoinText.style.opacity = 0;
    }
}
