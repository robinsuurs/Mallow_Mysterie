using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMainMenuStart : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    
    private void Start() {
        if (!PlayerPrefs.HasKey("Master")) {
            PlayerPrefs.SetFloat("Master", 9);
            PlayerPrefs.SetFloat("Music", 9);
            PlayerPrefs.SetFloat("Effects", 9);
            PlayerPrefs.Save();
        }
        
        ChangeMasterVolume(PlayerPrefs.GetFloat("Master"));
        ChangeMusicVolume(PlayerPrefs.GetFloat("Music"));
        ChangeEffectVolume(PlayerPrefs.GetFloat("Effects"));
    }

    private void ChangeMasterVolume(float volumeLevel) {
        masterMixer.SetFloat("Master", volumeLevel == 0 ? -80 : Mathf.Log10((volumeLevel + 1)/10) * 20);
    }

    private void ChangeMusicVolume(float volumeLevel) {
        masterMixer.SetFloat("Music", volumeLevel == 0 ? -80 : Mathf.Log10((volumeLevel + 1)/10) * 20);
    }

    private void ChangeEffectVolume(float volumeLevel) {
        masterMixer.SetFloat("Effects", volumeLevel == 0 ? -80 : Mathf.Log10((volumeLevel + 1)/10) * 20);
    }
}
