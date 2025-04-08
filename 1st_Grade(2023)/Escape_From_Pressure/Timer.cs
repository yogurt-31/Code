using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private float _minute, _second;
    [SerializeField] private int day;
    [SerializeField] private AudioClip[] knockClips;
    private float _time = 0;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_minute <= 0 && Mathf.Round(_second - _time) <= 0 )
        {
            SceneManager.LoadScene("Jumpscare");
            return;
        }
        _time += Time.deltaTime;

        string _secondTotal = Mathf.Round(_second - _time).ToString();

        if (Mathf.Round(_second - _time) < 10) _secondTotal = "0" + _secondTotal;

        if (_minute < 10)
        {
            _timerText.text = "<size=40>" + day + "老瞒</size>\n0" + _minute + ":" + _secondTotal;
        }
        else
        {
            _timerText.text = "<size=40>" + day + "老瞒</size>\n0" + _minute + ":" + Mathf.Round(_second - _time);
        }
        KnockSound();

        if (_second - _time <= 0 )
        {
            if(_minute > 0)
            {
                _second = 60;
                _time = 0;
                _minute--;
            }
        }
    }

    private void KnockSound()
    {
        if (_timerText.text == "<size=40>" + day + "老瞒</size>\n02:00")
        {
            _audioSource.clip = knockClips[0];
            _audioSource.Play();
        }
        else if (_timerText.text == "<size=40>" + day + "老瞒</size>\n01:00")
        {
            _audioSource.clip = knockClips[1];
            _audioSource.Play();
        }
        else if (_timerText.text == "<size=40>" + day + "老瞒</size>\n00:30")
        {
            _audioSource.clip = knockClips[2];
            _audioSource.Play();
        }
        else if (_timerText.text == "<size=40>" + day + "老瞒</size>\n00:10")
        {
            _audioSource.clip = knockClips[3];
            _audioSource.Play();
        }
        else if (_timerText.text == "<size=40>" + day + "老瞒</size>\n00:05")
        {
            _audioSource.clip = knockClips[4];
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
