using System.Collections.Generic;
using System.Linq;
using Dialogue.RunTime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

[System.Serializable] 
public class GameData {
    public List<ItemDataSave> itemDataSaves = new List<ItemDataSave>();
    public List<PermissionCheckSave> PermissionCheckSaves = new List<PermissionCheckSave>();
    public List<string> alreadyHadConversations = new List<string>();
    public float timeRun;
    public string sceneName;
    public Vector3 playerLocation;
    public int beerDrunk;

    public bool cameraHasPanned;

    public SerializableDictionary<string, string> questionAnswerDic =
        new SerializableDictionary<string, string>();
    
    //Set start thing when you create a newGame
    public GameData(string leaveEmpty) {

        var inventory = Resources.LoadAll("Clues/ClueInventory", typeof(Inventory))
            .Cast<Inventory>().FirstOrDefault(inventoryArray => inventoryArray.name.Equals("ClueInventory"));
        
        foreach (var item in inventory.items) {
            item.hasBeenPickedUp = false;
            item.pickedUpNumber = 0;
        }
        
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("Dialogues").ToList();
        foreach (var dialogue in dialogueContainers) {
            dialogue.alreadyHadConversation = false;
        }
        
        List<Answer> answers = Resources.LoadAll<Answer>("QuestionAnswers/Answers").ToList();
        foreach (var answer in answers) {
            answer.OnEnable();
        }
        
        List<PermissionCheck> permissions = Resources.LoadAll<PermissionCheck>("Permission").ToList();
        foreach (var permission in permissions) {
            permission.setPermission(false);
        }
        
        List<Question> questions = Resources.LoadAll<Question>("QuestionAnswers/Questions").ToList();
        foreach (var question in questions) {
            question.setChosenAnswer(null);
        }
        
        questionAnswerDic.Clear();
        timeRun = 0;

        cameraHasPanned = false;

        beerDrunk = 0;
        
        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.2433f, 0.2f);
    }
}
