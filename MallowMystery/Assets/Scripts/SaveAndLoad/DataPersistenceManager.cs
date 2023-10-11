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
    [SerializeField] private GameEventStandardAdd gameEventStandardAdd;
    //TODO: Change this shit:
    [SerializeField] private Inventory _inventory;
    
    private GameData _gameData;
    private List<IDataPersistence> dataPersistences;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one DataPersistenceManager found, Shit hits the fan! Or Destroying the new one");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
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
        this._gameData = new GameData(_inventory);
    }

    public void LoadGame() {
        if (this._gameData == null) {
            this._gameData = dataHandler.Load();
        }
        if (this._gameData == null || startFresh) {
            Debug.Log("No GameData found. A new Game needs to be created");
            return;
        }
        else {
            foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
                dataPersistenceObj.LoadData(_gameData);
            }
            gameEventStandardAdd.Raise();
        }
    }

    public void SaveGame () {
        if (this._gameData == null) {
            Debug.Log("No GameData found. A new Game needs to be created before being saved");
            return;
        }
        
        foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
            dataPersistenceObj.SaveData(ref _gameData);
        }

        _gameData.Scene = SceneManager.GetActiveScene();
        
        dataHandler.Save(_gameData);
    }

    public bool hasGameData() {
        return _gameData != null;
    }
}
