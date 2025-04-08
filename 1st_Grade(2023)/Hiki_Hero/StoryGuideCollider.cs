using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGuideCollider : MonoBehaviour
{
    DialogueManager manager;
    [SerializeField] private float number;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if(manager.i >= number) GetComponent<BoxCollider2D>().isTrigger = true;
        else GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
