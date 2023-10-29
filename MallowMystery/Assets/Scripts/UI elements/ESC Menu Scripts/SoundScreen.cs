using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundScreen : MonoBehaviour{
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider effectVolume;
    [SerializeField] private AudioMixer masterMixer;

    private void Start() {
        if (!PlayerPrefs.HasKey("masterVolume")) {
            PlayerPrefs.SetFloat("masterVolume", 1);
            PlayerPrefs.SetFloat("effectVolume", 1);
            PlayerPrefs.SetFloat("musicVolume", 1);
        } else {
            Load();
        }
        
    }

    public void ChangeMasterVolume(float volumeLevel) {
        masterMixer.SetFloat("masterVolume", volumeLevel);
        PlayerPrefs.SetFloat("masterVolume", volumeLevel);
    }
    
    public void ChangeMusicVolume(float volumeLevel) {
        masterMixer.SetFloat("musicVolume", volumeLevel);
        PlayerPrefs.SetFloat("musicVolume", volumeLevel);
    }
    
    public void ChangeEffectVolume(float volumeLevel) {
        masterMixer.SetFloat("effectVolume", volumeLevel);
        PlayerPrefs.SetFloat("effectVolume", volumeLevel);
    }

    private void Load() {
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        effectVolume.value = PlayerPrefs.GetFloat("effectVolume");
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    
}
