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
    public SceneSwitchData sceneSwitchData;
    [SerializeField] private GameObject playerPrefab;

    public void SpawnPlayer(GameData gameData) {
        bool spawnedPlayer = false;
        List<GameObject> spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation").ToList();
        if (sceneSwitchData != null) {
            foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals(sceneSwitchData.playerSpawnLocationName))) {
                Instantiate(playerPrefab, spawn.gameObject.transform.position, quaternion.identity);
                spawnedPlayer = true;
                break;
            }
        }
        else {
            if (!gameData.sceneName.Equals(SceneManager.GetActiveScene().name)) {
                if (spawnLocations.Exists(spawn => spawn.name.Equals("TestSpawn"))) {
                    foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals("TestSpawn"))) {
                        Instantiate(playerPrefab, spawn.transform.position, quaternion.identity); //This is for Testing purposes
                        spawnedPlayer = true;
                        break;
                    }
                }
                
            } else {
                Instantiate(playerPrefab, gameData.playerLocation, quaternion.identity);
                spawnedPlayer = true;
            }
        }

        if (spawnedPlayer) return;
        Debug.Log("No spawn location found, spawning player at 0,0,0");
        Instantiate(playerPrefab, new Vector3(0,0,0), quaternion.identity);
    }
    
    //TODO BM: 29-10-2023 Look into async load of scene, has to be in monobehaviour because of coroutine
    public void LoadNextScene(SceneSwitchData sceneSwitchData)
    {
        Time.timeScale = 0;
        this.sceneSwitchData = sceneSwitchData;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(sceneSwitchData.sceneName);
        Time.timeScale = 1;
    }
}
