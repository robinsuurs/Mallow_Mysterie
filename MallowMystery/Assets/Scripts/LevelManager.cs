using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Manager/LevelManager")]
public class LevelManager : ScriptableObject {
    private List<GameObject> spawnLocations;
    public SceneSwitchData sceneSwitchData;
    [SerializeField] private GameObject playerPrefab;
    // private string newSpawnLocation;
    public PlayerSpawnLocation playerSpawnLocation;

    public void SpawnPlayer() {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation").ToList();
        if (sceneSwitchData != null) {
            foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals(sceneSwitchData.playerSpawnLocationName))) {
                Instantiate(playerPrefab, spawn.gameObject.transform.position, quaternion.identity);
                break;
            }
        }
        else {
            Instantiate(playerPrefab);
        }
    }
    
    public void LoadNextScene(SceneSwitchData sceneSwitchData)
    {
        Time.timeScale = 0;
        this.sceneSwitchData = sceneSwitchData;
        
        SceneManager.LoadScene(sceneSwitchData.sceneName);
        Time.timeScale = 1;
        
        // StartCoroutine(LoadNextSceneAfterSeconds(3));
        // LoadNextSceneAfterSeconds(3);
    }

    // IEnumerator LoadNextSceneAfterSeconds(int seconds)
    // {
    //     yield return new WaitForSecondsRealtime(seconds);
    //     SceneManager.LoadScene(sceneSwitchData.sceneName);
    //     Time.timeScale = 1;
    // }
    
}
