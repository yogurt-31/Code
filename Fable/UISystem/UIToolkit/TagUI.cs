using UnityEngine.UIElements;

public class TagUI : UIPanel
{
    public void OpenPanel()
    {
        VisualElement tagPanel = MainUI.Instance.TagPanel;
        tagPanel.style.display = DisplayStyle.Flex;
    }

    public void ClosePanel()
    {
        VisualElement tagPanel = MainUI.Instance.TagPanel;
        tagPanel.style.display = DisplayStyle.None;
    }
}
