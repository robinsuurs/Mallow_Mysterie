using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dialogue.RunTime;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

//Youtube video used: https://www.youtube.com/watch?v=aUi9aijvpgs&t=538s

public class DataPersistenceManager : MonoBehaviour {
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    [SerializeField] private bool startFresh;
    [SerializeField] private bool encryptData;
    [SerializeField] private UnityEvent endLoading;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private EndingStringList endingStringList;
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
        DontDestroyOnLoad(gameObject);
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        if (_gameData == null) {
            _gameData = dataHandler.Load();
        } else {
            NewGame();
        }
        _levelManager.sceneSwitchData = null;
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu")) {
            LoadGame();
            _levelManager.SpawnPlayer(_gameData);
        }
        endLoading.Invoke();
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") && !SceneManager.GetActiveScene().name.Equals("DetectiveRoom") && !SceneManager.GetActiveScene().name.Equals("EndingScene")) {
            Camera.main.gameObject.GetComponent<Follow_Player>().setFollowPlayer();
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceMon = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        IEnumerable<IDataPersistence> dataPersistenceScript = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceMon.Concat(dataPersistenceScript));
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    public void NewGame() {
        this._gameData = new GameData("");
    }

    public void LoadGame() {
        if (this._gameData == null || startFresh) {
            Debug.Log("No GameData found. A new Game needs to be created");
            NewGame();
        }
        else {
            dataPersistences = FindAllDataPersistenceObjects();
            foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
                dataPersistenceObj.LoadData(_gameData);
            }

            LoadDialogueStates();
        }
    }

    public void setFromMainMenu () {
        this._levelManager.sceneSwitchData = null;
    }

    public string getSceneToLoadForMainMenu() {
        return _gameData.sceneName;
    }

    public void SaveGame () {
        if (this._gameData == null) {
            Debug.Log("No GameData found. A new Game needs to be created before being saved");
            return;
        }

        if (SceneManager.GetActiveScene().name.Equals("MainMenu") || SceneManager.GetActiveScene().name.Equals("EndingScene")) {
            return;
        }

        dataPersistences = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
            dataPersistenceObj.SaveData(ref _gameData);
        }
        
        SaveDialogueStates();

        _gameData.sceneName = SceneManager.GetActiveScene().name;
        _gameData.playerLocation = GameObject.FindWithTag("Player").transform.position;
        
        dataHandler.Save(_gameData);
    }

    public bool hasGameData() {
        return _gameData != null;
    }

    private void SaveDialogueStates() {
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        _gameData.alreadyHadConversations.Clear();

        foreach (var dialogueContainer in dialogueContainers.Where(dialogueContainer => dialogueContainer.alreadyHadConversation)) {
            _gameData.alreadyHadConversations.Add(dialogueContainer.name);
        }
    }
    
    private void LoadDialogueStates() {
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        foreach (var dialogueContainer in from dialogueContainer in dialogueContainers from name in _gameData.alreadyHadConversations where dialogueContainer.name.Equals(name) select dialogueContainer) {
            dialogueContainer.alreadyHadConversation = true;
        }
    }

    public void resetToStandardValues() {
        _levelManager.sceneSwitchData = null;
    }

    public void setEndingStringList(EndingStringList endingStringList) {
        this.endingStringList = endingStringList;
    }

    public EndingStringList GetEndingStringList() {
        return endingStringList;
    }
}
