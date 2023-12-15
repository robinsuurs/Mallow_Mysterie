using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetQuestions : MonoBehaviour {
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Question question;

    private void OnEnable() {
        refreshDropDown();
    }

    private void refreshDropDown() {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(new List<string> { "Select an answer" });
        _dropdown.AddOptions(question.getAnswers().Where(answer => answer.getEnabled()).Select(answer => answer.getAnswer()).OrderBy(s => s).ToList());

        if (question.getChosenAnswer() == null) return;
        for (int i = 0; i < _dropdown.options.Count; i++) {
            if (!_dropdown.options[i].text.Equals(question.getChosenAnswer().answer)) continue;
            _dropdown.SetValueWithoutNotify(i);
            break;
        }
    }

    public void saveAnswer(int number) {
        if (number == 0) {
            question.setChosenAnswer(null);
        }
        question.setChosenAnswer(_dropdown.options[number].text);
    }
}
