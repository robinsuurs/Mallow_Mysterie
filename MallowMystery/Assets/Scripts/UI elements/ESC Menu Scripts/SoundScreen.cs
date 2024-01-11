using UnityEngine;
using UnityEngine.Audio;

public class SoundScreen : MonoBehaviour{
    [SerializeField] private AudioMixer masterMixer;

    private void Start() {
        if (!PlayerPrefs.HasKey("masterVolume")) {
            PlayerPrefs.SetFloat("masterVolume", 9);
            PlayerPrefs.SetFloat("effectVolume", 9);
            PlayerPrefs.SetFloat("musicVolume", 9);
        }
    }

    public void ChangeMasterVolume(float volumeLevel) {
        masterMixer.SetFloat("masterVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
        PlayerPrefs.SetFloat("masterVolume", volumeLevel);
    }
    
    public void ChangeMusicVolume(float volumeLevel) {
        masterMixer.SetFloat("musicVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
        PlayerPrefs.SetFloat("musicVolume", volumeLevel);
    }
    
    public void ChangeEffectVolume(float volumeLevel) {
        masterMixer.SetFloat("effectVolume", volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
        PlayerPrefs.SetFloat("effectVolume", volumeLevel);
    }
}
