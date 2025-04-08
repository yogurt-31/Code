using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public struct ItemElement
{
    public Button button;
    public int price;
}
public class RhythmUI : UIPanel
{
    #region 변수

    [SerializeField] private GoGameScene goGameScene;
    [SerializeField] private Popup itemPopupUI;
    [SerializeField] private Sprite defaultStarImage;
    [SerializeField] private Sprite yellowStarImage;
    [SerializeField] private Sprite blueStarImage;
    [SerializeField] private Sprite pinkStarImage;
    [SerializeField] private StoryPanel storyPanel;

    private VisualElement root;
    private VisualElement noTouchZone;

    private VisualElement Thumbnail;
    private VisualElement recordElement;
    private TextElement recordText;

    private VisualElement titleElement;
    private TextElement songNameText;
    private TextElement choiceText;

    private VisualElement fairyLevel;
    private Button fairyButton;
    private TextElement fairyLevelText;

    private VisualElement dreamLevel;
    private Button dreamButton;
    private TextElement dreamLevelText;

    private VisualElement nightmareLevel;
    private Button nightmareButton;
    private TextElement nightmareLevelText;

    private Button startButton;
    private VisualElement ticketImage;
    private TextElement noStartText;

    private VisualElement leftStar;
    private VisualElement middleStar;
    private VisualElement rightStar;

    private Button storyButton;

    private VisualElement itemElement;
    private ItemElement knightItem;
    private ItemElement fairyItem;
    private ItemElement bookItem;
    private ItemElement storyItem;
    private ItemElement resetItem;

    private Vector2 lastMousePosition;

    private int selectItemPrice = 0;

    private bool isOpen = false;
    private bool isNoStart = false;

    #endregion

    private void OnEnable()
    {
        selectItemPrice = 0;
        SettingPanel();
        SettingItemPanel();
    }
    private void Update()
    {
        if (!isOpen) return;

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 deltaPosition = currentMousePosition - lastMousePosition;

            if (deltaPosition.magnitude > 50f)
            {
                if (deltaPosition.y < 0f)
                {
                    DisableAllLevelButton();
                    OpenPanel(true);
                }

                return;
            }
            lastMousePosition = currentMousePosition;
        }


        Touch touch = new Touch();
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if (touch.deltaPosition.magnitude > 50f)
        {
            if (touch.deltaPosition.y < 0f)
            {
                DisableAllLevelButton();
                OpenPanel(true);
            }
        }

        if((Information.Instance.GameData.Coin - selectItemPrice) < 0)
        {
            isNoStart = true;
            if (Information.Instance.IsKorean)
                noStartText.text = "코인이 부족해요!";
            else
                noStartText.text = "not enough Coin!";
            noStartText.style.visibility = Visibility.Visible;
        }
        else if (Information.Instance.currentDiff == DifficultType.Nightmare && Information.Instance.GameData.EnterTicket <= 0)
        {
            isNoStart = true;
            if (Information.Instance.IsKorean)
                noStartText.text = "티켓이 부족해요!";
            else
                noStartText.text = "not enough Ticket";
            noStartText.style.visibility = Visibility.Visible;
        }
        else
        {
            isNoStart = false;
            noStartText.style.visibility = Visibility.Hidden;
        }
    }

    private void SettingItemPanel()
    {
        itemElement = root.Q<VisualElement>("ItemElement");

        knightItem.button = itemElement.Q<Button>("KnightBtn");
        fairyItem.button = itemElement.Q<Button>("FairyBtn");
        bookItem.button = itemElement.Q<Button>("BookBtn");
        storyItem.button = itemElement.Q<Button>("StoryBtn");

        knightItem.price = -300; // 용사의 가호
        fairyItem.price = -250; // 요정의 가호
        bookItem.price = -1500; // 지식의 가호
        storyItem.price = -400; // 동화의 가호
        resetItem.price = 0;

        HandleItemClick(resetItem);

        Button popupButton = itemElement.Q<Button>("PopupBtn");
        popupButton.RegisterCallback<ClickEvent>((evt) =>
        {
            itemPopupUI.OpenTutorialPanel();
        });


        knightItem.button.RegisterCallback<ClickEvent>((evt) =>
        {
            Information.Instance.UseHeroItem = !Information.Instance.UseHeroItem;
            knightItem.price *= -1;
            if (Information.Instance.UseHeroItem) knightItem.button.AddToClassList("click");
            else knightItem.button.RemoveFromClassList("click");
            HandleItemClick(knightItem);
        });
        fairyItem.button.RegisterCallback<ClickEvent>((evt) =>
        {
            Information.Instance.UseFairyItem = !Information.Instance.UseFairyItem;
            fairyItem.price *= -1;
            if (Information.Instance.UseFairyItem) fairyItem.button.AddToClassList("click");
            else fairyItem.button.RemoveFromClassList("click");
            HandleItemClick(fairyItem);
        });
        bookItem.button.RegisterCallback<ClickEvent>((evt) =>
        {
            Information.Instance.UseKnowledgeItem = !Information.Instance.UseKnowledgeItem;
            bookItem.price *= -1;
            if (Information.Instance.UseKnowledgeItem) bookItem.button.AddToClassList("click");
            else bookItem.button.RemoveFromClassList("click");
            HandleItemClick(bookItem);
        });
        storyItem.button.RegisterCallback<ClickEvent>((evt) =>
        {
            Information.Instance.UseFairytaleItem = !Information.Instance.UseFairytaleItem;
            storyItem.price *= -1;
            if (Information.Instance.UseFairytaleItem) storyItem.button.AddToClassList("click");
            else storyItem.button.RemoveFromClassList("click");
            HandleItemClick(storyItem);
        });
    }

    private void HandleItemClick(ItemElement item)
    {
        selectItemPrice += item.price;
        TextElement calculateText = itemElement.Q<TextElement>("CalculateTxt");
        TextElement totalText = itemElement.Q<TextElement>("TotalTxt");

        string priceText = string.Empty;
        if (selectItemPrice != 0) priceText = "\n-" + selectItemPrice;
        else priceText = "\n";

        calculateText.text = Information.Instance.GameData.Coin + priceText;
        totalText.text = (Information.Instance.GameData.Coin - selectItemPrice).ToString();
    }

    private void SettingPanel()
    {
        root = MainUI.Instance.rhythmPanel.Q<VisualElement>("Root");
        noTouchZone = MainUI.Instance.root.Q<VisualElement>("RhythmNoTouchZone");

        Thumbnail = root.Q<VisualElement>("Thumbnail");
        recordElement = root.Q<VisualElement>("RecordElement");
        recordText = recordElement.Q<TextElement>("RecordTxt");

        titleElement = root.Q<VisualElement>("TitleElement");
        songNameText = titleElement.Q<TextElement>("SongNameTxt");

        fairyLevel = titleElement.Query<VisualElement>("Fairy");
        fairyButton = fairyLevel.Q<Button>("FairyBtn");
        fairyLevelText = fairyLevel.Q<TextElement>("LvTxt");
        fairyButton.clicked += HandleFairyButtonClick;

        dreamLevel = titleElement.Query<VisualElement>("Dream");
        dreamButton = dreamLevel.Q<Button>("DreamBtn");
        dreamLevelText = dreamLevel.Q<TextElement>("LvTxt");
        dreamButton.clicked += HandleDreamButtonClick;

        nightmareLevel = titleElement.Query<VisualElement>("Nightmare");
        nightmareButton = nightmareLevel.Q<Button>("NightmareBtn");
        nightmareLevelText = nightmareLevel.Q<TextElement>("LvTxt");
        nightmareButton.clicked += HandleNightmareButtonClick;

        choiceText = titleElement.Q<TextElement>("ChoiceText");

        if (Information.Instance.IsKorean)
            choiceText.text = "난이도 선택";
        else
            choiceText.text = "Diffcult Choice";
        // 아이템도 추가해야함
        startButton = root.Q<Button>("StartBtn");
        noStartText = startButton.Q<TextElement>("NoStartTxt");
        ticketImage = startButton.Q<VisualElement>("TicketImg");

        leftStar = recordElement.Q<VisualElement>("LeftStar");
        middleStar = recordElement.Q<VisualElement>("MiddleStar");
        rightStar = recordElement.Q<VisualElement>("RightStar");

        storyButton = root.Q<Button>("StoryBtn");
        Debug.Log(storyButton);

        if (Information.Instance.IsKorean)
            storyButton.text = "스토리 보기";
        else
            storyButton.text = "View Story";

        storyButton.clicked += HandleClick;
    }

    private void HandleClick()
    {
        Debug.Log("아니 ㅁㅁㅇㄻㄴㅇㄻㄴ");
        storyPanel.gameObject.SetActive(true);
    }

    private void HandleGameStart(ClickEvent evt)
    {
        if (isNoStart) return;
        startButton.UnregisterCallback<ClickEvent>(HandleGameStart);

        if (Information.Instance.currentDiff == DifficultType.Nightmare)
            --Information.Instance.GameData.EnterTicket;
        else
        {
            if (Information.Instance.currentSong.SongBPM == 0) return;
            ticketImage.style.visibility = Visibility.Hidden;
        }
        if (Information.Instance.GameData.Coin - selectItemPrice < 0) return;
        Information.Instance.GameData.Coin -= selectItemPrice;

        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => MainUI.Instance.FadeImage(false));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => SceneManager.LoadScene("RhythmGame"));
        //StartCoroutine(LobbyManager.instance.LobbyGO.GameScene());
    }

    private void HandleFairyButtonClick()
    {
        isNoStart = false;
        DisableAllLevelButton();
        fairyButton.AddToClassList("on");

        Information.Instance.currentDiff = DifficultType.Fairy;
        OpenPanel(false);

        recordText.text = $"{Information.Instance.GameData.BestFairytaleAccuraries[Information.Instance.currentSong.SongID].ToString("F2")}%";
        SettingRecord(Information.Instance.GameData.BestStarRatingFairytale[Information.Instance.currentSong.SongID]);

        if (Information.Instance.currentSong.SongBPM == 0) return;
        ticketImage.style.visibility = Visibility.Hidden;
        noStartText.style.visibility = Visibility.Hidden;
    }

    private void HandleDreamButtonClick()
    {
        isNoStart = false;
        DisableAllLevelButton();
        dreamButton.AddToClassList("on");

        Information.Instance.currentDiff = DifficultType.Dream;
        OpenPanel(false);

        recordText.text = $"{Information.Instance.GameData.BestDreamAccuraries[Information.Instance.currentSong.SongID].ToString("F2")}%";
        SettingRecord(Information.Instance.GameData.BestStarRatingDream[Information.Instance.currentSong.SongID]);

        if (Information.Instance.currentSong.SongBPM == 0) return;
        ticketImage.style.visibility = Visibility.Hidden;
        noStartText.style.visibility = Visibility.Hidden;
    }

    private void HandleNightmareButtonClick()
    {
        DisableAllLevelButton();
        nightmareButton.AddToClassList("on");


        OpenPanel(false);

        recordText.text = $"{Information.Instance.GameData.BestNightmareAccuraries[Information.Instance.currentSong.SongID].ToString("F2")}%";
        SettingRecord(Information.Instance.GameData.BestStarRatingNightmare[Information.Instance.currentSong.SongID]);

        ticketImage.style.visibility = Visibility.Visible;
        Information.Instance.currentDiff = DifficultType.Nightmare;
    }

    private void DisableAllLevelButton()
    {
        fairyButton.RemoveFromClassList("on");
        dreamButton.RemoveFromClassList("on");
        nightmareButton.RemoveFromClassList("on");
    }

    public void SettingRecord(int num)
    {
        switch (num)
        {
            case 0:
                SettingStarImage(defaultStarImage.texture, defaultStarImage.texture, defaultStarImage.texture);
                break;
            case 1:
                SettingStarImage(defaultStarImage.texture, yellowStarImage.texture, defaultStarImage.texture);
                break;
            case 2:
                SettingStarImage(yellowStarImage.texture, yellowStarImage.texture, defaultStarImage.texture);
                break;
            case 3:
                SettingStarImage(yellowStarImage.texture, yellowStarImage.texture, yellowStarImage.texture);
                break;
            case 4:
                SettingStarImage(blueStarImage.texture, blueStarImage.texture, blueStarImage.texture);
                break;
            case 5:
                SettingStarImage(pinkStarImage.texture, pinkStarImage.texture, pinkStarImage.texture);
                break;
        }
    }

    private void SettingStarImage(Texture2D leftTexture, Texture2D middleTexture, Texture2D rightTexture)
    {
        leftStar.style.backgroundImage = new StyleBackground(leftTexture);
        middleStar.style.backgroundImage = new StyleBackground(middleTexture);
        rightStar.style.backgroundImage = new StyleBackground(rightTexture);
    }
    public void OpenPanel(bool isHalf)
    {
        if (isAnimating) return;
        isAnimating = true;
        string addString = "halfup", removeString = "up";
        if (!isHalf)
        {
            (addString, removeString) = (removeString, addString);

            recordElement.RemoveFromClassList("off");
        }
        else
        {
            recordElement.AddToClassList("off");
        }
        isOpen = !isHalf;

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            MainUI.Instance.rhythmPanel.RemoveFromClassList(removeString);
            MainUI.Instance.rhythmPanel.AddToClassList(addString);
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            noTouchZone.style.visibility = Visibility.Visible;
            noTouchZone.style.display = DisplayStyle.Flex;
            noTouchZone.RegisterCallback<ClickEvent>((evt) => ClosePanel());
            isAnimating = false;
        });


        PanelInformation();
    }

    private void PanelInformation()
    {
        Song song = Information.Instance.currentSong;

        if (song.Thumbnail != null)
            Thumbnail.style.backgroundImage = new StyleBackground(song.Thumbnail.texture);
        else
            Thumbnail.style.backgroundImage = null;

        if(Information.Instance.IsKorean)
            songNameText.text = song.SongName;
        else
            songNameText.text = song.EngSongName;

        fairyLevelText.text = "Lv." + song.FairytaleDiffcult.ToString();
        dreamLevelText.text = "Lv." + song.DreamDifficult.ToString();
        nightmareLevelText.text = "Lv." + song.NightMareDifficult.ToString();

        startButton.RegisterCallback<ClickEvent>(HandleGameStart);
    }

    public void ClosePanel()
    {
        if (isAnimating) return;
        isAnimating = true;
        noTouchZone.UnregisterCallback<ClickEvent>((evt) => ClosePanel());

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            LobbyManager.Instance.SetCamPosition.Cancel();
            MainUI.Instance.rhythmPanel.RemoveFromClassList("halfup");
            MainUI.Instance.rhythmPanel.RemoveFromClassList("up");
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            noTouchZone.style.visibility = Visibility.Hidden;
            noTouchZone.style.display = DisplayStyle.None;
            LobbyManager.Instance.SelectType = SelectType.NoSelect;
            isAnimating = false;
            MainUI.Instance.isBlinking = true;
            MainUI.Instance.BlinkArrow();
        });
    }
}
