using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private List<GameObject> spawnLocations;
    private SceneSwitchData sceneSwitchData;
    [SerializeField] private GameObject playerPrefab;
    private string newSpawnLocation;

    public void SwitchScene(string location) {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation").ToList();
        foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals(location))) {
            Instantiate(playerPrefab, spawn.gameObject.transform.position, quaternion.identity);
            break;
        }
    }
    
    public void LoadNextScene(SceneSwitchData sceneSwitchData)
    {
        Time.timeScale = 0;
        StartCoroutine(LoadNextSceneAfterSeconds(3));
    }

    IEnumerator LoadNextSceneAfterSeconds(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneSwitchData.sceneName);
        Time.timeScale = 1;
    }
    
}
