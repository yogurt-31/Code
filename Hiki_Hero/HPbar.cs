using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }
    void Update()
    {
        if (FindObjectOfType<DialogueManager>().i >= 70) GetComponent<CanvasGroup>().alpha = 1f;
    }
}
