using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;

    public AudioSource[] soundFx;

    public AudioSource bgm, titleScreen;

    private void Awake() {
        instance = this; 
    }

    private void Start() {
        SetDefaultValue();        
    }

    public void SetDefaultValue() {
        float bgmVol = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float sfxVol = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
    
        Debug.Log(bgmVol);

        mixer.SetFloat("MusicVol", Mathf.Log10(bgmVol) * 20);
        mixer.SetFloat("SFXVol", Mathf.Log10(sfxVol) * 20);
    
    }

    public void PlayBGM() {
        titleScreen.Stop();

        bgm.Play();
    }

    public void PlayTitleScreenBgm() {
        bgm.Stop();

        titleScreen.Play();
    }

    public void PlaySFX(int soundToPlay) {
        soundFx[soundToPlay].Stop();

        soundFx[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        soundFx[soundToPlay].Play();
    }
}
