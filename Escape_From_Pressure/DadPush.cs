using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DadPush : MonoBehaviour
{
    private Animator _animator;
    private bool isEnter = false;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Doorend"))
        {
            if (isEnter) return;
            isEnter = true;
            _animator.SetBool("Run", false);
            gameObject.GetComponent<AudioSource>().Stop();
            _animator.SetBool("Push", true);
            FinalDoorEvent.Instance.StartCoroutine(FinalDoorEvent.Instance.DoorEvent());
            other.gameObject.GetComponent<AudioSource>().Play();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Jumpscare");
        }
    }
}
