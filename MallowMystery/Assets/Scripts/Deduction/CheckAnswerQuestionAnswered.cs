using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CheckAnswerQuestionAnswered : MonoBehaviour {
    [SerializeField] private List<Question> questions;
    [SerializeField] private UnityEvent questionsCorrect;
    [SerializeField] private UnityEvent questionsNotCorrect;
    
    public void CheckAnswer() {
        if (questions.Any(question => question.getChosenAnswer() == null)) {
            questionsNotCorrect.Invoke();
            return;
        }

        questionsCorrect.Invoke();
    }
}
