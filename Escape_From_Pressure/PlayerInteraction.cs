using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private TextMeshProUGUI _fText;
    public void SetActiveInteractionPanel(bool _setActive, string _interactionText = "")
    {
        _interactionPanel.SetActive(_setActive);
        _fText.text = "F - " + _interactionText;
    }
}
