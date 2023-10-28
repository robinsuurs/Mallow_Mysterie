using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider effectVolume;
    [SerializeField] private Slider musicVolume;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("masterVolume")) {
            PlayerPrefs.SetFloat("masterVolume", 1);
            PlayerPrefs.SetFloat("effectVolume", 1);
            PlayerPrefs.SetFloat("musicVolume", 1);
        } 
        else {
            Load();
        }
    }

    private void Load() {
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
    }
}
