using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {
    [SerializeField] private SettingsScreenManager settingsScreenManager;

    private void Start() {
        settingsScreenManager = GameObject.FindWithTag("CanvasManager").gameObject.transform.Find("SettingsScreen").gameObject.GetComponent<SettingsScreenManager>();
    }

    void OnOpenSettings() {
        settingsScreenManager.showSettingsScreen("OpenSettings");
    }

    void OnOpenInventory() {
        settingsScreenManager.showSettingsScreen("Inventory");
    }

    void OnOpenMap() {
        if (SceneManager.GetActiveScene().name.Equals("OverworldMap")) {
            settingsScreenManager.showSettingsScreen("MapOverlay");   
        }
    }
}
