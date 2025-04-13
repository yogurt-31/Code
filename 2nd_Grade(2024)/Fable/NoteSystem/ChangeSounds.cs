using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeSounds : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider bgmVolume;
    [SerializeField] private Slider sfxVolume;

    private float master;
    private float bgm;
    private float sfx;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            VolumeReset();
        }
        else
        {
            GetVolumeData();
        }
    }

    private void VolumeReset()
    {
        PlayerPrefs.SetFloat("MasterVolume", master);
        PlayerPrefs.SetFloat("BGM", bgm);
        PlayerPrefs.SetFloat("SFX", sfx);
    }

    private void GetVolumeData()
    {
        master = PlayerPrefs.GetFloat("MasterVolume");
        bgm = PlayerPrefs.GetFloat("BGM");
        sfx = PlayerPrefs.GetFloat("SFX");
    }

    private void Start()
    {
        audioMixer.SetFloat("MasterVolume", master);
        audioMixer.SetFloat("BGM", bgm);
        audioMixer.SetFloat("SFX", sfx);

        masterVolume.value = master;
        bgmVolume.value = bgm;
        sfxVolume.value = sfx;
    }

    public void MasterVolumeChange()
    {
        master = masterVolume.value;
        ChangeVolume("MasterVolume", master);
    }

    public void BGMVolumeChange()
    {
        bgm = bgmVolume.value;
        ChangeVolume("BGM", bgm);
    }

    public void SFXVolumeChange()
    {
        sfx = sfxVolume.value;
        ChangeVolume("SFX", sfx);
    }

    private void ChangeVolume(string type, float volume)
    {
        PlayerPrefs.SetFloat(type, volume);
        audioMixer.SetFloat(type, volume);
    }
}
