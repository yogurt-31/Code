using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private AnimationCurve resultCurve;
    [SerializeField] private TextMeshProUGUI levelUpText;

    [SerializeField] private Sprite fairySprite;
    [SerializeField] private Sprite dreamSprite;
    [SerializeField] private Sprite nightmareSprite;

    private UIDocument uiDocument;

    private VisualElement rootElement;
    private VisualElement topElement;
    private VisualElement judgementElement;
    private VisualElement coinElement;
    private VisualElement expElement;

    private VisualElement oneStarElem;
    private VisualElement twoStarElem;
    private VisualElement threeStarElem;

    private Button nextButton;

    private Label gameTitleLabel;
    private Label accuraryLabel;

    private VisualElement difficultElement;
    private Label dreamLabel;
    private Label coolLabel;
    private Label bedLabel;
    private Label awakeLabel;

    private Label coinLabel;
    private Label expLabel;

    private float accurary;

    private int dream;
    private int cool;
    private int bed;
    private int awake;

    private int plusCoin;
    private int plusExp;

    private bool isOneStar;
    private bool isTwoStar;
    private bool isThreeStar;

    private bool isFC;
    private bool isAP;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        rootElement = uiDocument.rootVisualElement;
        topElement = rootElement.Q<VisualElement>("TopPanel");
        judgementElement = rootElement.Q<VisualElement>("JudgementElement");
        coinElement = rootElement.Q<VisualElement>("CoinPanel");
        expElement = rootElement.Q<VisualElement>("XPElement");

        oneStarElem = topElement.Q<VisualElement>("MiddleStar");
        twoStarElem = topElement.Q<VisualElement>("LeftStar");
        threeStarElem = topElement.Q<VisualElement>("RightStar");

        nextButton = rootElement.Q<Button>("NextBtn");
        nextButton.style.display = DisplayStyle.None;
        nextButton.pickingMode = PickingMode.Ignore;

        nextButton.clicked += () =>
        {
            Information.Instance.SetUpItemfalse();
            SceneManager.LoadScene("Lobby");
        };

        gameTitleLabel = topElement.Q<Label>("GameTitleTxt");

        if (Information.Instance.IsKorean)
            gameTitleLabel.text = Information.Instance.currentSong.SongName;
        else
            gameTitleLabel.text = Information.Instance.currentSong.EngSongName;
        accuraryLabel = topElement.Q<Label>("GamePercentTxt");

        difficultElement = judgementElement.Q<VisualElement>("DiffElement");
        ChangeDiffElement(Information.Instance.currentDiff);

        dreamLabel = judgementElement.Q<Label>("DreamTxt");
        coolLabel = judgementElement.Q<Label>("CoolTxt");
        bedLabel = judgementElement.Q<Label>("BedTxt");
        awakeLabel = judgementElement.Q<Label>("AwakeTxt");

        coinLabel = coinElement.Q<Label>("CoinTxt");
        expLabel = expElement.Q<Label>("XPTxt");

        SetValue();
        CalculateResult();
        StartCoroutine(ResultCo());
    }

    private void ChangeDiffElement(DifficultType currentDiff)
    {
        StyleBackground style;
        if (currentDiff == DifficultType.Fairy)
            style = new StyleBackground(fairySprite.texture);
        else if (currentDiff == DifficultType.Dream)
            style = new StyleBackground(dreamSprite.texture);
        else
            style = new StyleBackground(nightmareSprite.texture);

        difficultElement.style.backgroundImage = style;
    }

    private void SetValue()
    {
        dream = Information.Instance.dream;
        cool = Information.Instance.cool;
        bed = Information.Instance.bed;
        awake = Information.Instance.awake;
    }

    private void CalculateResult()
    {
        float sumJudge
            = dream * 100
            + cool * 65
            + bed * 35
            + awake * 0;

        float sumNote
            = dream
            + cool
            + bed
            + awake;

        accurary = sumJudge / sumNote;

        int diffcult = 0;

        switch (Information.Instance.currentDiff)
        {
            case DifficultType.Fairy: diffcult = Information.Instance.currentSong.FairytaleDiffcult; break;
            case DifficultType.Dream: diffcult = Information.Instance.currentSong.DreamDifficult; break;
            case DifficultType.Nightmare: diffcult = Information.Instance.currentSong.NightMareDifficult; break;
        }

        plusCoin = (int)(accurary * ((5 + diffcult) / 10f));
        plusExp = (int)(4 * accurary * ((5 + diffcult) / 10f));

        if(Information.Instance.UseKnowledgeItem)
        {
            Information.Instance.UseKnowledgeItem = false;
            plusExp *= 2;
        }

        SetPlayData();
    }

    private void SetPlayData()
    {
        Information.Instance.GameData.Coin += plusCoin;
        Information.Instance.GameData.Exp += plusExp;

        if (Information.Instance.GameData.Exp < 1000)
            return;

        while (Information.Instance.GameData.Exp >= 1000)
        {
            Information.Instance.GameData.LV++;
            Information.Instance.GameData.EnterTicket++;
            Information.Instance.GameData.Exp -= 1000;
        }
        StartCoroutine(LevelUpCo());

        Task.Run(() => Ranking());
    }

    private IEnumerator LevelUpCo()
    {
        yield return new WaitForSeconds(0.25f);
        levelUpText.text = "Level Up!";
        yield return new WaitForSeconds(1f);
        levelUpText.text = "";
    }

    private async Task Ranking()
    {
        await Task.Run(() =>
        {
            BackEndManager.Instance.SetRanking();
            BackEndManager.Instance.GetRanking();
        });
    }

    private IEnumerator ResultCo()
    {
        float co_accurary = 0f;

        int co_dream = 0;
        int co_cool = 0;
        int co_bed = 0;
        int co_awake = 0;

        int co_coin = 0;
        int co_exp = 0;

        float t = 0f;
        float lerpTime = 5f;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            yield return null;

            if (Input.touchCount >= 1 || Input.GetMouseButtonDown(0))
            {
                t = 5f;
            }

            co_accurary = Mathf.Lerp(0f, accurary, resultCurve.Evaluate(t / lerpTime));
            accuraryLabel.text = $"{co_accurary.ToString("0.00")}%";

            co_dream = (int)Mathf.Lerp(0f, dream, resultCurve.Evaluate(t / lerpTime));
            dreamLabel.text = $"{co_dream}";

            co_cool = (int)Mathf.Lerp(0f, cool, resultCurve.Evaluate(t / lerpTime));
            coolLabel.text = $"{co_cool}";

            co_bed = (int)Mathf.Lerp(0f, bed, resultCurve.Evaluate(t / lerpTime));
            bedLabel.text = $"{co_bed}";

            co_awake = (int)Mathf.Lerp(0f, awake, resultCurve.Evaluate(t / lerpTime));
            awakeLabel.text = $"{co_awake}";

            co_coin = (int)Mathf.Lerp(0f, plusCoin, resultCurve.Evaluate(t / lerpTime));
            coinLabel.text = $"{co_coin}";

            co_exp = (int)Mathf.Lerp(0f, plusExp, resultCurve.Evaluate(t / lerpTime));
            expLabel.text = $"{co_exp}";

            ClassUpStar(co_accurary);
        }
        ClassUpStar(co_accurary);
        FCAP();
        UpdateData();
        nextButton.style.display = DisplayStyle.Flex;
        nextButton.pickingMode = PickingMode.Position;
    }

    private void ClassUpStar(float accu)
    {
        if (isOneStar == false)
        {
            if (accu >= 50f)
            {
                isOneStar = true;
                oneStarElem.AddToClassList("clear");
            }
        }

        if (isTwoStar == false)
        {
            if (accu >= 70f)
            {
                isTwoStar = true;
                twoStarElem.AddToClassList("clear");
            }
        }

        if (isThreeStar == false)
        {
            if (accu >= 90f)
            {
                isThreeStar = true;
                threeStarElem.AddToClassList("clear");
            }
        }
    }

    private void FCAP()
    {
        isFC = awake == 0 && isThreeStar;
        isAP = accurary == 100f;

        if (isAP)
        {
            oneStarElem.AddToClassList("AP");
            twoStarElem.AddToClassList("AP");
            threeStarElem.AddToClassList("AP");
        }
        else if (isFC)
        {
            oneStarElem.AddToClassList("FC");
            twoStarElem.AddToClassList("FC");
            threeStarElem.AddToClassList("FC");
        }
    }

    private void UpdateData()
    {
        GameData gameData = Information.Instance.GameData;
        int songID = Information.Instance.currentSong.SongID;

        switch (Information.Instance.currentDiff)
        {
            case DifficultType.Fairy:
                {
                    if (gameData.BestFairytaleAccuraries[songID] < accurary)
                    {
                        gameData.BestFairytaleAccuraries[songID] = accurary;
                        gameData.BestStarRatingFairytale[songID] = GetStarData();
                    }
                }
                break;
            case DifficultType.Dream:
                {
                    if (gameData.BestDreamAccuraries[songID] < accurary)
                    {
                        gameData.BestDreamAccuraries[songID] = accurary;
                        gameData.BestStarRatingDream[songID] = GetStarData();
                    }
                }
                break;
            case DifficultType.Nightmare:
                {
                    if (gameData.BestNightmareAccuraries[songID] < accurary)
                    {
                        gameData.BestNightmareAccuraries[songID] = accurary;
                        gameData.BestStarRatingNightmare[songID] = GetStarData();
                    }
                }
                break;
        }
    }

    private int GetStarData()
    {
        int star = 0;
        if (isOneStar) star++;
        if (isTwoStar) star++;
        if (isThreeStar) star++;
        if (isFC) star++;
        if (isAP) star++;

        return star;
    }

    #region OldCode
    //private UIDocument document;
    //private VisualElement root;
    //private VisualElement topPanel;
    //private Button nextButton;

    //private float accurary = 0f;

    //private bool isOneStar = false;
    //private bool isTwoStar = false;
    //private bool isThreeStar = false;
    //private bool isAP = false;
    //private bool isFC = false;

    //private void OnEnable()
    //{
    //    document = GetComponent<UIDocument>();
    //    root = document.rootVisualElement;
    //    topPanel = root.Q<VisualElement>("TopPanel");

    //    ClearUpdate();
    //    WhatIsDifficult();
    //    RecordSetting();
    //    StarSetting(isOneStar, isTwoStar, isThreeStar);
    //    JudgeSetting();

    //    nextButton = root.Q<Button>("NextBtn");
    //    nextButton.RegisterCallback<ClickEvent>(HandleButtonClick);
    //}

    //private void HandleButtonClick(ClickEvent evt)
    //{
    //    nextButton.UnregisterCallback<ClickEvent>(HandleButtonClick);
    //    SceneManager.LoadScene("Lobby");
    //}

    //private void JudgeSetting()
    //{
    //    VisualElement bottomPanel = root.Q<VisualElement>("BottomPanel");

    //    TextElement dreamText = bottomPanel.Q<TextElement>("DreamTxt");
    //    TextElement coolText = bottomPanel.Q<TextElement>("CoolTxt");
    //    TextElement bedText = bottomPanel.Q<TextElement>("BedTxt");
    //    TextElement awakeText = bottomPanel.Q<TextElement>("AwakeTxt");

    //    int perfect = Information.Instance.dream;
    //    int great = Information.Instance.cool;
    //    int bad = Information.Instance.bed;
    //    int miss = Information.Instance.awake;

    //    DOTween.To(() => 0, x => dreamText.text = x.ToString(), perfect, 4f);
    //    DOTween.To(() => 0, x => coolText.text = x.ToString(), great, 4f);
    //    DOTween.To(() => 0, x => bedText.text = x.ToString(), bad, 4f);
    //    DOTween.To(() => 0, x => awakeText.text = x.ToString(), miss, 4f);
    //}

    //private void StarSetting(bool isMiddleStar, bool isLeftStar, bool isRightStar)
    //{
    //    VisualElement middleStar = root.Q<VisualElement>("MiddleStar");
    //    VisualElement leftStar = root.Q<VisualElement>("LeftStar");
    //    VisualElement rightStar = root.Q<VisualElement>("RightStar");

    //    Sequence seq = DOTween.Sequence();
    //    seq.AppendInterval(1f);
    //    seq.AppendCallback(() =>
    //    {
    //        if (isMiddleStar)
    //            middleStar.AddToClassList("clear");
    //    });
    //    seq.AppendInterval(1f);
    //    seq.AppendCallback(() =>
    //    {
    //        if (isLeftStar)
    //            leftStar.AddToClassList("clear");
    //    });
    //    seq.AppendInterval(1f);
    //    seq.AppendCallback(() =>
    //    {
    //        if (isRightStar)
    //            rightStar.AddToClassList("clear");
    //    });
    //    seq.AppendInterval(1f);
    //    seq.AppendCallback(() =>
    //    {
    //        if (isFC)
    //        {
    //            middleStar.AddToClassList("FC");
    //            leftStar.AddToClassList("FC");
    //            rightStar.AddToClassList("FC");
    //        }
    //        if(isAP)
    //        {
    //            middleStar.AddToClassList("AP");
    //            leftStar.AddToClassList("AP");
    //            rightStar.AddToClassList("AP");
    //        }
    //    });
    //}

    //private void RecordSetting()
    //{

    //    TextElement titleText = topPanel.Q<TextElement>("GameTitleTxt");
    //    TextElement percentText = topPanel.Q<TextElement>("GamePercentTxt");

    //    titleText.text = Information.Instance.currentSong.SongName;
    //    accurary = CalculateAccu();
    //    CalculateStarRating(accurary);
    //    //percentText.text = accurary.ToString("F2") + "%";
    //    DOTween.To(() => 0f, x => percentText.text = x.ToString("F2") + "%", accurary, 4f).SetEase(Ease.OutExpo);

    //}

    //private void CalculateStarRating(float accurary)
    //{

    //    if (accurary < 50f) return;

    //    if(accurary >= 50f)
    //    {
    //        isOneStar = true;
    //        if(accurary >= 70f)
    //        {
    //            isTwoStar = true;
    //            if(accurary >= 90f)
    //            {
    //                isThreeStar = true;

    //            }
    //        }
    //    }

    //    if (Information.Instance.awake == 0)
    //        isFC = true;

    //    if (this.accurary == 100f)
    //        isAP = true;
    //}

    //private static void ClearUpdate()
    //{
    //    if (Information.Instance.currentDiff == DifficultType.Fairy)
    //        Information.Instance.GameData.IsFairytaleClear[Information.Instance.currentSong.SongID] = true;
    //    else if (Information.Instance.currentDiff == DifficultType.Dream)
    //        Information.Instance.GameData.IsDreamClear[Information.Instance.currentSong.SongID] = true;
    //    else
    //        Information.Instance.GameData.IsNightClear[Information.Instance.currentSong.SongID] = true;
    //}

    //private void WhatIsDifficult()
    //{
    //    TextElement lvText = root.Q<TextElement>("LvTxt");
    //    if (Information.Instance.currentDiff == DifficultType.Fairy)
    //        lvText.text = "Fairy";
    //    else if (Information.Instance.currentDiff == DifficultType.Dream)
    //        lvText.text = "Dream";
    //    else
    //        lvText.text = "Nightmare";
    //}

    //private float CalculateAccu()
    //{
    //    float sumJudge
    //       = Information.Instance.dream * 100
    //       + Information.Instance.cool * 65
    //       + Information.Instance.bed * 35
    //       + Information.Instance.awake * 0;

    //    float sumNote
    //        = Information.Instance.dream
    //        + Information.Instance.cool
    //        + Information.Instance.bed
    //        + Information.Instance.awake;

    //    return sumJudge / sumNote;
    //}

    //private void UpdateRecord()
    //{
    //    /*int id = Information.Instance.currentSong.SongID;
    //    if (Information.Instance.currentDiff == DifficultType.Fairy)
    //    {
    //        if (Information.Instance.GameData.BestFairytaleAccuraries[id] < accurary)
    //        {
    //            Information.Instance.GameData.BestFairytaleAccuraries[id] = accurary;
    //        }
    //        else
    //        {
    //            return;
    //        }

    //        if (isAP)
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 5;
    //        }
    //        else if (isFC)
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 4;
    //        }
    //        else if (isThreeStar)
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 3;
    //        }
    //        else if (isTwoStar)
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 2;
    //        }
    //        else if (isOneStar)
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 1;
    //        }
    //        else
    //        {
    //            Information.Instance.GameData.BestStarRatingFairytale[id] = 0;
    //        }
    //    }
    //    else if (Information.Instance.currentDiff == DifficultType.Dream)
    //    {
    //        if (Information.Instance.GameData.BestDreamAccuraries[id] < accurary)
    //        {
    //            Information.Instance.GameData.BestDreamAccuraries[id] = accurary;
    //        }
    //        else
    //        {
    //            return;
    //        }

    //        if (isAP)
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 5;
    //        }
    //        else if (isFC)
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 4;
    //        }
    //        else if (accurary > 90f)
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 3;
    //        }
    //        else if (accurary > 70f)
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 2;
    //        }
    //        else if (accurary > 50f)
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 1;
    //        }
    //        else
    //        {
    //            Information.Instance.GameData.BestStarRatingDream[id] = 0;
    //        }
    //    }
    //    else
    //    {
    //        if (Information.Instance.GameData.BestNightmareAccuraries[id] < accurary)
    //        {
    //            Information.Instance.GameData.BestNightmareAccuraries[id] = accurary;
    //        }
    //        else
    //        {
    //            return;
    //        }

    //        if (isAP)
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 5;
    //        }
    //        else if (isFC)
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 4;
    //        }
    //        else if (accurary > 90f)
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 3;
    //        }
    //        else if (accurary > 70f)
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 2;
    //        }
    //        else if (accurary > 50f)
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 1;
    //        }
    //        else
    //        {
    //            Information.Instance.GameData.BestStarRatingNightmare[id] = 0;
    //        }
    //    }*/
    #endregion
}