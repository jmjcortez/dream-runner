using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public bool isMusicSlider;

    private void Start() {
        if (isMusicSlider) {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        }
        else {
            slider.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        }
    }

    public void SetBgmLevel(float sliderValue) {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSfxLevel(float sliderValue) {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SfxVolume", sliderValue);
    }
}
