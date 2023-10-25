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

    public void SpawnPlayer(GameData gameData) {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation").ToList();
        if (sceneSwitchData != null) {
            foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals(sceneSwitchData.playerSpawnLocationName))) {
                Instantiate(playerPrefab, spawn.gameObject.transform.position, quaternion.identity);
                break;
            }
        }
        else {
            if (DataPersistenceManager.instance.getStartFresh() || !gameData.sceneName.Equals(SceneManager.GetActiveScene().name)) {
                foreach (var spawn in GameObject.FindGameObjectsWithTag("SpawnLocation")
                             .Where(spawn => spawn.name.Equals("TestSpawn"))) {
                    Instantiate(playerPrefab, spawn.transform.position, quaternion.identity); //This is for Testing purposes
                    break;
                }
            } else {
                Instantiate(playerPrefab, gameData.playerLocation, quaternion.identity);
            }
        }
    }
    
    public void LoadNextScene(SceneSwitchData sceneSwitchData)
    {
        Time.timeScale = 0;
        this.sceneSwitchData = sceneSwitchData;
        
        SceneManager.LoadScene(sceneSwitchData.sceneName);
        Time.timeScale = 1;
    }
}
