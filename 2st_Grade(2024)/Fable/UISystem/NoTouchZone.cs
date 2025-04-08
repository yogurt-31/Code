using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoTouchZone : MonoBehaviour, IPointerClickHandler
{
    private PanelSystem panelSystem;

    private void Awake()
    {
        panelSystem = transform.parent.GetChild(1).GetComponent<PanelSystem>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        panelSystem.ClosePanel(gameObject.GetComponent<Image>());
    }
}
