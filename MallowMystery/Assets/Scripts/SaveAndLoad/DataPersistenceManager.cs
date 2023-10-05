using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ScriptObjects;
using UnityEngine.SceneManagement;

//Youtube video used: https://www.youtube.com/watch?v=aUi9aijvpgs&t=538s

public class DataPersistenceManager : MonoBehaviour {
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    [SerializeField] private bool startFresh;
    [SerializeField] private bool encryptData;
    
    private GameData _gameData;
    private List<IDataPersistence> dataPersistences;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one DataPersistenceManager found, Shit hits the fan!");
        }

        instance = this;
    }

    private void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        this.dataPersistences = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistences = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<IDataPersistence>();
        IEnumerable<IDataPersistence> dataPersistences2 = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistences.Concat(dataPersistences2));
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    public void NewGame() {
        this._gameData = new GameData();
    }

    public void LoadGame() {
        this._gameData = dataHandler.Load();
        if (this._gameData == null || startFresh) {
            Debug.Log("No GameData found, creating new GameData");
            NewGame();
        }
        else {
            foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
                dataPersistenceObj.LoadData(_gameData);
            }

            SceneManager.SetActiveScene(_gameData.Scene);
        }
    }

    public void SaveGame () {
        foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
            dataPersistenceObj.SaveData(ref _gameData);
        }

        _gameData.Scene = SceneManager.GetActiveScene();
        
        dataHandler.Save(_gameData);
    }
}
