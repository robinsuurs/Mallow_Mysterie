using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimePlayedTrack : MonoBehaviour, IDataPersistence {
    private string currentSceneName;
    public static float currentTimeRun;
    private void Update() {
        if (currentSceneName != "MainMenu" && currentSceneName != "EndingScene") {
            currentTimeRun += Time.unscaledDeltaTime;
        }
    }

    public void setCurrentSceneName() {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void LoadData(GameData data) {
        currentTimeRun = data.timeRun;
    }

    public void SaveData(ref GameData data) {
        data.timeRun = currentTimeRun;
    }
}
