using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundFx;

    public AudioSource bgm, titleScreen;

    private void Awake() {
        instance = this;        
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
