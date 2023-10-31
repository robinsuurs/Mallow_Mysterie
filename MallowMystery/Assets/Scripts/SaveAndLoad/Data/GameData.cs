using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Runtime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable] 
public class GameData {
    public List<ItemDataSave> itemDataSaves = new List<ItemDataSave>();
    public List<string> alreadyHadConversations = new List<string>();
    public Inventory inventory;
    public string sceneName;
    public Vector3 playerLocation;
    public ProgressionEnum.gameProgression gameProgression;
    
    //Set start thing when you create a newGame
    public GameData(string leaveEmpty) {

        this.inventory = Resources.LoadAll("Clues/ClueInventory", typeof(Inventory))
            .Cast<Inventory>().FirstOrDefault(inventoryArray => inventoryArray.name.Equals("ClueInventory"));
        
        foreach (var item in inventory.items) {
            item.hasBeenPickedUp = false;
            item.pickedUpNumber = 0;
        }
        
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("Dialogues").ToList();
        foreach (var dialogue in dialogueContainers) {
            dialogue.alreadyHadConversation = false;
        }
        
        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.3522396f, 0.2f);
        gameProgression = ProgressionEnum.gameProgression.talkToDetectiveInOffice;
    }
}
