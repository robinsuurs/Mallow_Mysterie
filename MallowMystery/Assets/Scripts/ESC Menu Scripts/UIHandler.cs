using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    [SerializeField] private GameObject settingsScreen;

    private void Start() {
        settingsScreen = GameObject.FindWithTag("CanvasManager").gameObject.transform.Find("SettingsScreen").gameObject;
    }

    void OnOpenSettings() {
        settingsScreen.GetComponent<SettingsScreenManager>().showSettingsScreen("OpenSettings");
    }

    void OnOpenInventory() {
        
    }
}
