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
    [SerializeField] private CanvasGroup canvas;
    private GameData _gameData;
    private GameData currentPlayingGameData;
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
        if (currentPlayingGameData == null) {
            currentPlayingGameData = dataHandler.Load();
            _gameData = currentPlayingGameData;
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
        canvas.alpha = 0;
        canvas.gameObject.SetActive(false);
        if (startFresh) {
            NewGame();
            LoadGame();
        }
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") && !SceneManager.GetActiveScene().name.Equals("EndingScene")) {
            LoadGame();
            _levelManager.SpawnPlayer(currentPlayingGameData);
        }
        endLoading.Invoke();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceMon = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        IEnumerable<IDataPersistence> dataPersistenceScript = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceMon.Concat(dataPersistenceScript));
    }

    private void OnApplicationQuit() {
        SaveToPc();
    }

    public void NewGame() {
        currentPlayingGameData = new GameData("");
        
        dataPersistences = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
            dataPersistenceObj.LoadData(currentPlayingGameData);
        }

        LoadDialogueStates();
    }

    public void LoadGame() {
        if (currentPlayingGameData == null) {
            Debug.Log("No GameData found. A new Game needs to be created");
            NewGame();
        }
        else {
            dataPersistences = FindAllDataPersistenceObjects();
            foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
                dataPersistenceObj.LoadData(currentPlayingGameData);
            }

            LoadDialogueStates();
        }
    }

    public void LoadGameFromPc() {
        currentPlayingGameData = dataHandler.Load();
        LoadGame();
    }

    public void setFromMainMenu () {
        this._levelManager.sceneSwitchData = null;
    }

    public string getSceneToLoadForMainMenu() {
        return currentPlayingGameData.sceneName;
    }

    public void SaveGame () {
        if (currentPlayingGameData == null) {
            Debug.Log("No GameData found. A new Game needs to be created before being saved");
            return;
        }

        if (SceneManager.GetActiveScene().name.Equals("MainMenu") || SceneManager.GetActiveScene().name.Equals("EndingScene")) {
            return;
        }

        dataPersistences = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataPersistences) {
            dataPersistenceObj.SaveData(ref currentPlayingGameData);
        }
        
        SaveDialogueStates();
        SavePlayer();
    }

    public void SaveToPc() {
        SaveGame();
        dataHandler.Save(currentPlayingGameData);
    }

    private void SavePlayer() {
        currentPlayingGameData.sceneName = SceneManager.GetActiveScene().name;
        if (GameObject.FindWithTag("Player") != null) {
            currentPlayingGameData.playerLocation = GameObject.FindWithTag("Player").transform.position;
        }
    }

    public bool hasGameData() {
        return _gameData != null;
    }

    private void SaveDialogueStates() {
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        currentPlayingGameData.alreadyHadConversations.Clear();

        foreach (var dialogueContainer in dialogueContainers.Where(dialogueContainer => dialogueContainer.alreadyHadConversation)) {
            currentPlayingGameData.alreadyHadConversations.Add(dialogueContainer.name);
        }
    }
    
    private void LoadDialogueStates() {
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        foreach (var dialogueContainer in from dialogueContainer in dialogueContainers from name in currentPlayingGameData.alreadyHadConversations where dialogueContainer.name.Equals(name) select dialogueContainer) {
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
