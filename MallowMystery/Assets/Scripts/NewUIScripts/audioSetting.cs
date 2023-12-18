using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class audioSetting : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text handleText;
    [SerializeField] private AudioMixer audioMixer;

    private void Start()
    {
        ValueChanged(PlayerPrefs.GetFloat(audioMixer.name));
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
        audioMixer.SetFloat(audioMixer.name, volumeLevel == 0 ? -80 : Mathf.Log10(volumeLevel/10) * 20);
        PlayerPrefs.SetFloat(audioMixer.name, volumeLevel);
    }
}
