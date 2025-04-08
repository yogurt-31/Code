using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class RankingUI : UIPanel
{
    private VisualElement rankingPanel;
    private Tween tween;
    private TextElement titleText;

    private void OnEnable()
    {
        rankingPanel = MainUI.Instance.rankingPanel;
        titleText = rankingPanel.Q<TextElement>("LevelRankingTxt");
        if (Information.Instance.IsKorean)
            titleText.text = "���� ��ŷ";
        else
            titleText.text = "Level Ranking";
    }
    public override void OpenPanel(VisualElement panel)
    {
        base.OpenPanel(panel);
        UpdateRankingBoard();
    }

    private void UpdateRankingBoard()
    {
        VisualElement rankingBoard = rankingPanel.Q<VisualElement>("RankingBoard");
        var rankingChildren = rankingBoard.Children().ToList();

        List<RankingData> list = Information.Instance.rankingDatas.OrderBy(r => int.Parse(r.rank)).ToList();
        // Update top 5 ranks
        for (int i = 0; i < 5; ++i)
        {
            TextElement rankingText = rankingChildren[i].Query<TextElement>("RankingTxt");
            if (i < list.Count)
            {
                rankingText.text = $"{list[i].rank}�� | {list[i].name} | LV.{list[i].score}";
            }
            else
            {
                rankingText.text = "������ �����ϴ�.";
            }
        }

        // Highlight player's own rank
        TextElement playerRankText = rankingPanel.Q<VisualElement>("MyRank").Q<TextElement>("RankingTxt");
        RankingData rankingData = Information.Instance.myRankingData;
        if(rankingData.name != "")
        {
            playerRankText.text = $"{rankingData.rank}�� | {rankingData.name} | LV.{rankingData.score}";
        }
    }
}
