using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUI : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGM");
            SoundSetting("BGM", bgmSlider.value);
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
            SoundSetting("SFX", bgmSlider.value);
        }
    }

    public void SetBGMLevel(float sliderVal)
    {
        SoundSetting("BGM", sliderVal);
        PlayerPrefs.SetFloat("BGM", sliderVal);
    }
    public void SetSFXLevel(float sliderVal)
    {
        SoundSetting("SFX", sliderVal);
        PlayerPrefs.SetFloat("SFX", sliderVal);
    }

    private void SoundSetting(string str, float val)
        => mixer.SetFloat(str, Mathf.Log10(val) * 20);
}
