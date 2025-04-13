using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SliderInfo
{
    public VisualElement root;
    public TextElement sliderText;
    public SliderInt slider;
}

public class CustomSlider : MonoBehaviour
{
    private SliderInfo masterVolume;
    private SliderInfo bgmVolume;
    private SliderInfo sfxVolume;

    [SerializeField] private AudioMixer audioMixer;

    private void OnEnable()
    {
        masterVolume = new SliderInfo();
        bgmVolume = new SliderInfo();
        sfxVolume = new SliderInfo();

        Initialize(masterVolume, "MasterVolume", "전체 음량", "MasterVolume");
        Initialize(bgmVolume, "BGMVolume", "배경음", "BGM"); 
        Initialize(sfxVolume, "SFXVolume", "효과음", "SFX"); 

        masterVolume.slider.RegisterCallback<ChangeEvent<int>>(MasterSliderValueChanged);
        bgmVolume.slider.RegisterCallback<ChangeEvent<int>>(BGMSliderValueChanged);
        sfxVolume.slider.RegisterCallback<ChangeEvent<int>>(SFXSliderValueChanged);
    }

    private void Initialize(SliderInfo slider, string volumeText, string labelText, string engLabelText)
    {
        slider.root = MainUI.Instance.optionPanel.Query<VisualElement>(volumeText);
        slider.sliderText = slider.root.Q<TextElement>("SliderTxt");
        slider.slider = slider.root.Q<SliderInt>("SliderInt");

        if (Information.Instance.IsKorean)
            slider.sliderText.text = labelText;
        else
            slider.sliderText.text = engLabelText;

        if(PlayerPrefs.HasKey(volumeText))
        {
            slider.slider.value = PlayerPrefs.GetInt(volumeText);
            audioMixer.SetFloat(volumeText, slider.slider.value);
        }
    }

    private void MasterSliderValueChanged(ChangeEvent<int> value)
    {
        audioMixer.SetFloat("MasterVolume", value.newValue);
        PlayerPrefs.SetInt("MasterVolume", value.newValue);
    }

    private void BGMSliderValueChanged(ChangeEvent<int> value)
    {
        audioMixer.SetFloat("BGMVolume", value.newValue);
        PlayerPrefs.SetInt("BGMVolume", value.newValue);
    }

    private void SFXSliderValueChanged(ChangeEvent<int> value)
    {
        audioMixer.SetFloat("SFXVolume", value.newValue);
        PlayerPrefs.SetInt("SFXVolume", value.newValue);
    }
}