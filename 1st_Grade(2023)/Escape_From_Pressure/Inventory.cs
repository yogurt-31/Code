using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Image[] inventoryImage;
    public bool[] hasObject;

    private void Awake()
    {
        hasObject = new bool[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            inventoryImage[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    public void InventoryImageSetActive(int _getObjectNumber)
    {
        inventoryImage[_getObjectNumber].enabled = true;
        hasObject[_getObjectNumber] = true;
    }
    public void InventoryImageDestroy(int _getObjectNumber)
    {
        inventoryImage[_getObjectNumber].enabled = false;
        hasObject[_getObjectNumber] = false;
    }
}
