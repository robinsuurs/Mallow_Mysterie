using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneSwitching : MonoBehaviour {
    [SerializeField] private SceneSwitchData sceneSwitchData;
    
    public void LoadNextScene()
    {
        // TODO: Disable player movement
        // TODO: Play fade animation
        Time.timeScale = 0;
        StartCoroutine(LoadNextSceneAfterSeconds(3));
    }

    IEnumerator LoadNextSceneAfterSeconds(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneSwitchData.getSceneName());
        Time.timeScale = 1;
    }
}
