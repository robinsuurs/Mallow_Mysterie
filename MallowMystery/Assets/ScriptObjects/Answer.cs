using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SaveAndLoad.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Deduction/Answer")]
public class Answer : ScriptableObject, IDataPersistence {
    public string answer; //Needs to be public for dialogue
    public string UID; //Needs to be public for dialogue
    [SerializeField] private List<PickupEvent> pickupEvents;
    [SerializeField] private bool enabled = false;
    

    public void OnEnable() {
        foreach (var answerEvent in pickupEvents) {
            answerEvent.AddListener(this);
        }

        enabled = false;
    }

    public void OnEventTriggered() {
        enabled = true;
    }

    public string getAnswer() {
        return answer;
    }

    public bool getEnabled() {
        return enabled;
    }
    
    private void OnValidate() {
#if UNITY_EDITOR
        if (UID != "") return;
        UID = GUID.Generate().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void LoadData(GameData data) {
        foreach (var answer in data.answerSave.Where(answer => answer.UID.Equals(UID))) {
            enabled = answer.enabled;
            break;
        }
    }

    public void SaveData(ref GameData data) {
        foreach (var answer in data.answerSave.Where(answer => answer.UID.Equals(UID))) {
            answer.enabled = enabled;
            return;
        }
        data.answerSave.Add(new AnswerSave(UID, enabled));
    }
}
