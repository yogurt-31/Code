using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCanvas : MonoBehaviour
{
    private OptionPanel optionPanel;
    private ProfilePanel profilePanel;
    private ShopPanel shopPanel;

    public void OpenOptionPanel()
    {
        optionPanel.OptionBtn();
    }
}
