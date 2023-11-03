using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandlerObject : MonoBehaviour
{
    [SerializeField] private SettingsScreenManager settingsScreenManager;

    private void Start() {
        settingsScreenManager = GameObject.FindWithTag("CanvasManager").gameObject.transform.Find("SettingsScreen").gameObject.GetComponent<SettingsScreenManager>();
    }
    
    public void OpenSettings() {
        settingsScreenManager.showSettingsScreen("OpenSettings");
    }

    public void OpenInventory() {
        settingsScreenManager.showSettingsScreen("Inventory");
    }

    public void OpenMap() {
        if (SceneManager.GetActiveScene().name.Equals("OverworldMap")) {
            settingsScreenManager.showSettingsScreen("MapOverlay");   
        }
    }
}
