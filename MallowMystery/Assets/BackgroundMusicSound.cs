using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusicSound : MonoBehaviour {
    public AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float fadeSpeed;

    private void Start() {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    public void soundFadeOut() {
        StartCoroutine(soundFadeOutOverTime());
    }

    private IEnumerator soundFadeOutOverTime() {
        float originalSoundValue = audioSource.volume;
        float newSoundValue = 1;
        while (audioSource.volume > 0) {
            newSoundValue -= (fadeSpeed * Time.deltaTime);
            audioSource.volume = (newSoundValue - (fadeSpeed * Time.deltaTime)) * originalSoundValue;
            yield return null;
        }
    }
}
