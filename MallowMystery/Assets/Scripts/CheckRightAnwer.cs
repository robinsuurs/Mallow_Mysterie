using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckRightAnwer : MonoBehaviour {
    [SerializeField] private EventBool boolEvent;
    [SerializeField] private Question question;
    [SerializeField] private Answer answer;

    public void CheckWrongQuestion() {
        boolEvent.Raise(question.getChosenAnswer() != null && Equals(question.getChosenAnswer().UID, answer.UID));
    }
}
