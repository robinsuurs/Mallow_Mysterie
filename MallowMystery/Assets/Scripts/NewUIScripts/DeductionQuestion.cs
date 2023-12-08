using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
// using TMPro;

public class DeductionQuestion : MonoBehaviour
{
    [FormerlySerializedAs("_dropdownField")] [SerializeField] private TMP_Dropdown _dropdown;

    private void Start()
    {
        addAnswer("De hoeren");
    }

    public void addAnswer(string answer)
    {
        _dropdown.AddOptions(new List<string> { answer });
    }
}

