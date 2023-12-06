using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimePlayedTrack : MonoBehaviour, IDataPersistence {
    private string currentSceneName;
    [SerializeField] private float currentTimeRun;
    private void Update() {
        if (currentSceneName != "MainMenu") {
            currentTimeRun += Time.unscaledDeltaTime;
        }
    }

    private void setCurrentSceneName(Scene scene, LoadSceneMode mode) {
        currentSceneName = scene.name;
    }
    
    private void OnEnable() {
        SceneManager.sceneLoaded += setCurrentSceneName;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= setCurrentSceneName;
    }

    public void LoadData(GameData data) {
        currentTimeRun = data.timeRun;
    }

    public void SaveData(ref GameData data) {
        data.timeRun = currentTimeRun;
    }
}
