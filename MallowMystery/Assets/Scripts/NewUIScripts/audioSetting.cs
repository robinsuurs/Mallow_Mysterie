using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class audioSetting : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text handleText;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioMixerOutput audioMixerOutput;

    private void Start() {
        slider.value = PlayerPrefs.GetFloat(audioMixerOutput.ToString());
    }

    public void increase()
    {
        slider.value += 1;
    }

    public void decrease()
    {
        slider.value -= 1;
    }

    public void ValueChanged(float volumeLevel)
    {
        handleText.SetText(slider.value.ToString());
        audioMixer.SetFloat(audioMixerOutput.ToString(), volumeLevel == 0 ? -80 : Mathf.Log10((volumeLevel + 1)/10) * 20);
        PlayerPrefs.SetFloat(audioMixerOutput.ToString(), volumeLevel);
        PlayerPrefs.Save();
    }

    private enum AudioMixerOutput {
        Master,
        Music,
        Effects
    }
}
