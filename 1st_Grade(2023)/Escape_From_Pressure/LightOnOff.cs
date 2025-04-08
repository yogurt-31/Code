using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class LightOnOff : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private FindBGM _audioSource;
    [SerializeField] private GameObject eyes;
    private Screen _screen;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Stage4")
            _audioSource = FindObjectOfType<FindBGM>();
    }

    public IEnumerator OnOffLight()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < lights.Length; i++) lights[i].enabled = false;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
            for (int i = 0; i < lights.Length; i++) lights[i].enabled = true;
            yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
            for (int i = 0; i < 5; i++)
            {
                _audioSource.pitch = Random.Range(-2f, 2f);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            }
            
        }
        for (int i = 0; i < lights.Length; i++) lights[i].enabled = false;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < lights.Length; i++) lights[i].enabled = true;
        for (int i = 0; i < lights.Length; i++) lights[i].color = Color.red;
        _audioSource.pitch = 1f;
        eyes.SetActive(true);
        yield return null;
    }
}
