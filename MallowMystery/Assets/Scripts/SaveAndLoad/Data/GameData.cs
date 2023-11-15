using System.Collections.Generic;
using System.Linq;
using Dialogue.RunTime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

[System.Serializable] 
public class GameData {
    public List<ItemDataSave> itemDataSaves = new List<ItemDataSave>();
    public List<string> alreadyHadConversations = new List<string>();
    public string sceneName;
    public Vector3 playerLocation;
    
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
        
        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.2433f, 0.2f);
    }
}
