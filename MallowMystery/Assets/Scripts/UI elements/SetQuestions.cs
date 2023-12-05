using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetQuestions : MonoBehaviour {
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Question question;

    private void OnEnable() {
        foreach (var answer in question.getAnswers()) {
            answer.OnSelectedEvent += refreshDropDown;
        }
    }

    private void refreshDropDown() {
        _dropdown.ClearOptions();
        _dropdown.AddOptions(question.getAnswers().Where(answer => answer.getEnabled()).Select(answer => answer.getAnswer()).OrderBy(s => s).ToList());
        
        string answerUID;
        DataPersistenceManager.instance.getGameData().questionAnswerDic.TryGetValue(question.UID, out answerUID);

        if (answerUID == null) return;
        for (int i = 0; i < _dropdown.options.Count; i++) {
            if (!_dropdown.options[i].text.Equals(question.getAnswerStringBasedOnGUID(answerUID))) continue;
            _dropdown.SetValueWithoutNotify(i);
            break;
        }
    }

    public void saveAnswer(int number) {
        DataPersistenceManager.instance.getGameData().questionAnswerDic.Add(question.UID, question.getAnswerGUIDBasedOnString(_dropdown.options[number].text));
    }
}
