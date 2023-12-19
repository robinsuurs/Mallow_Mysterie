using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMainMenuStart : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    
    private void Start() {
        if (!PlayerPrefs.HasKey("masterVolume")) {
            PlayerPrefs.SetFloat("masterVolume", 9);
            PlayerPrefs.SetFloat("effectVolume", 9);
            PlayerPrefs.SetFloat("musicVolume", 9);
        }
        
        ChangeMasterVolume(PlayerPrefs.GetFloat("masterVolume"));
        ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        ChangeEffectVolume(PlayerPrefs.GetFloat("effectVolume"));
    }

    private void ChangeMasterVolume(float volumeLevel) {
        masterMixer.SetFloat("masterVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
    }

    private void ChangeMusicVolume(float volumeLevel) {
        masterMixer.SetFloat("musicVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
    }

    private void ChangeEffectVolume(float volumeLevel) {
        masterMixer.SetFloat("effectVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
    }
}
