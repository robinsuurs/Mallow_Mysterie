using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using TMPro;

public class audioSetting : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text handleText;

    private void Start()
    {
        ValueChanged();
    }

    public void increase()
    {
        slider.value += 1;
    }

    public void decrease()
    {
        slider.value -= 1;
    }

    public void ValueChanged()
    {
        handleText.SetText(slider.value.ToString());
    }
}
