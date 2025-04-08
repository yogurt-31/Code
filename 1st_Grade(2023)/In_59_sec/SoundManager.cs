using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(SceneCheck());
    }

    IEnumerator SceneCheck()
    {
        while(true)
        {
            if (SceneManager.GetActiveScene().name == "Stage-1" || SceneManager.GetActiveScene().name == "Stage-2")
            {
                StopMusic();
            }
            yield return null;
        }
    }
    void StopMusic()
    {
        Destroy(gameObject);
    }
}

