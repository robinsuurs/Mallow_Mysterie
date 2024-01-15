using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour, IDataPersistence { 
    [SerializeField] private string nameTutorial;
    [SerializeField] private List<InputActionReference> actions;
    [SerializeField] private FadeToBlackEvent showTutorial;
    [SerializeField] private UnityEvent actionPerformedEvent;
    private bool alreadyDone;
    private bool subscribed = false;

    private void Subscribe() {
        foreach (var action in actions) {
            action.action.performed += ActionPerformed;
        }
    }

    private void OnDisable() {
        foreach (var action in actions) {
            action.action.performed -= ActionPerformed;
        }
    }

    private void ActionPerformed(InputAction.CallbackContext callback) {
        alreadyDone = true;
        actionPerformedEvent.Invoke();
    }

    public void ShowTutorial() {
        if (alreadyDone || subscribed) return;
        
        showTutorial.Raise();
        Subscribe();
        subscribed = true;
    }

    public void LoadData(GameData data) {
        foreach (var saves in data.nameBoolSaves.Where(saves => saves.Key.Equals(nameTutorial))) {
            alreadyDone = saves.Value;
        }
    }

    public void SaveData(ref GameData data) {
        data.nameBoolSaves[nameTutorial] = alreadyDone;
    }
}
