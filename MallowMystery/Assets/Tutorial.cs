using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour, IDataPersistence {
    [SerializeField] private List<InputActionReference> action;
    private bool alreadyDone;

    public void showTutorial() {
        if (!alreadyDone) {
            
        }
    }

    public void LoadData(GameData data) {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data) {
        throw new System.NotImplementedException();
    }
}
