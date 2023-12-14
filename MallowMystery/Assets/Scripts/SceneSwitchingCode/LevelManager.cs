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
                instantiate(spawn.gameObject.transform.position);
                spawnedPlayer = true;
                break;
            }
        }
        else {
            //If testing and the scene doesn't correspond to the saved scene name
            if (!gameData.sceneName.Equals(SceneManager.GetActiveScene().name)) {
                if (spawnLocations.Exists(spawn => spawn.name.Equals("TestSpawn"))) {
                    foreach (var spawn in spawnLocations.Where(spawn => spawn.name.Equals("TestSpawn"))) {
                        instantiate(spawn.transform.position);
                        spawnedPlayer = true;
                        break;
                    }
                }
            } else {
                instantiate(gameData.playerLocation);
                spawnedPlayer = true;
            }
        }

        if (spawnedPlayer) return;
        Debug.Log("No spawn location found, spawning player at 0,0,0");
        instantiate(new Vector3(0,0,0));
    }
    
    public void LoadNextScene(SceneSwitchData sceneSwitchData)
    {
        Time.timeScale = 0;
        this.sceneSwitchData = sceneSwitchData;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(sceneSwitchData.sceneName);
        Time.timeScale = 1;
    }
    
    private void instantiate(Vector3 loc) {
        Instantiate(playerPrefab, loc, quaternion.identity);

        if (SceneManager.GetActiveScene().name.Equals("FriendsAppartment1")) {
            Camera.main.GetComponent<Follow_Player>().setFollowPlayer();
        }
    }
}
