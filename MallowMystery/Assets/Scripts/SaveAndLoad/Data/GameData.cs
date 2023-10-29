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
        List<ItemData> items = new List<ItemData>();
        var clueItems = Resources.LoadAll("Clues/PickupClues", typeof(ItemData)).Cast<ItemData>().ToArray();
        if (clueItems.Count() != 0) {
            foreach (var item in clueItems) {
                items.Add(item);
                item.hasBeenPickedUp = false;
                item.pickedUpNumber = 0;
            }
        }

        this.inventory = Resources.LoadAll("Clues/ClueInventory", typeof(Inventory))
            .Cast<Inventory>().FirstOrDefault(inventoryArray => inventoryArray.name.Equals("ClueInventory"));
        this.inventory.items = items;
        
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("Dialogues").ToList();
        foreach (var dialogue in dialogueContainers) {
            dialogue.alreadyHadConversation = false;
        }
        
        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.3522396f, 0.2f);
        gameProgression = ProgressionEnum.gameProgression.talkToDetectiveInOffice;
    }
}
