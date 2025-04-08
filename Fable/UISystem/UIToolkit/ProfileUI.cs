using DG.Tweening;
using UnityEngine.UIElements;

public class ProfileUI : UIPanel
{
    private Tween tween;

    private VisualElement profilePanel;
    private Button tagText;
    private TextElement profileText;
    private TextElement nicknameText;
    private TextElement levelText;
    private VisualElement emptyLevel;
    private VisualElement levelFill;
    private VisualElement levelPointer;

    private void OnEnable()
    {
        profilePanel = MainUI.Instance.profilePanel;

        tagText = profilePanel.Q<Button>("TagTxt");
        profileText = profilePanel.Q<TextElement>("ProfileTxt");
        nicknameText = profilePanel.Q<TextElement>("NameTxt");
        levelText = profilePanel.Q<TextElement>("LvTxt");
        emptyLevel = profilePanel.Q<VisualElement>("EmptyLevel");
        levelFill = profilePanel.Q<VisualElement>("LevelFill");
        levelPointer = profilePanel.Q<VisualElement>("LevelPointer");

        // Load game data
        GameData gameData = Information.Instance.GameData;

        if (Information.Instance.IsKorean)
            profileText.text = "ÇÁ·ÎÇÊ";
        else
            profileText.text = "Profile";

        tagText.RegisterCallback<ClickEvent>((evt) =>
        {
            MainUI.Instance.tagUI.OpenPanel();
        });
        tagText.text = Information.Instance.AchieveDicionary[gameData.selectedAchieve];
        nicknameText.text = gameData.Nickname;
        levelText.text = "Lv." + gameData.LV;
    }

    private void Update()
    {
        float fillPercentage = Information.Instance.GameData.Exp / 1000f;
        levelFill.style.width = emptyLevel.resolvedStyle.width * fillPercentage;
        levelPointer.style.left = emptyLevel.resolvedStyle.width * fillPercentage;
    }
}
