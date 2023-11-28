using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Deduction/Answer")]
public class Answer : ScriptableObject {
    public string answer; //Needs to be public for dialogue
    public string UID; //Needs to be public for dialogue
    [SerializeField] private List<AnswerEvent> answerEvents;
    private bool enabled = false;
    
    public delegate void SelectAction();
    public event SelectAction OnSelectedEvent;
    
    

    private void OnEnable() {
        foreach (var answerEvent in answerEvents) {
            answerEvent.AddListener(this);
        }

        enabled = false;
    }

    public void OnEventTriggered() {
        foreach (var answerEvent in answerEvents) {
            answerEvent.RemoveListener(this);
        }
        enabled = true;
        OnSelectedEvent?.Invoke();
    }

    public string getAnswer() {
        return answer;
    }

    public bool getEnabled() {
        return enabled;
    }

    public void setEnabledFalse() {
        enabled = false;
    }
    
    private void OnValidate() {
#if UNITY_EDITOR
        if (UID != "") return;
        UID = GUID.Generate().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
