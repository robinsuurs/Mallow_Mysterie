using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetQuestions : MonoBehaviour {
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private string nameOfAnswer;
    [SerializeField] private List<AnswerEvent> answerEvent;
    
    public void OnEventTriggered() {
        _dropdown.options.Add(new TMP_Dropdown.OptionData(nameOfAnswer));
        foreach (var answer in answerEvent) {
            answer.RemoveListener(this);
        }
    }
    
    private void OnDisable()
    {
        foreach (var answer in answerEvent) {
            answer.RemoveListener(this);
        }
    }

    private void OnEnable() {
        foreach (var answer in answerEvent) {
            answer.AddListener(this);
        }
    }
}
