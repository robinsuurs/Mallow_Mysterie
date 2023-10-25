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
    public List<ItemData> items;
    public List<ItemDataSave> ItemDataSaves = new List<ItemDataSave>();
    public Inventory inventory;
    public string sceneName;
    public Vector3 playerLocation;
    public List<DialogueNodeData> dialogues;
    public List<string> alreadyHadConversations = new List<string>();

    //Set start thing when you create a newGame
    public GameData(Inventory inventory) {
        this.items = new List<ItemData>();
        var clueItems = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ItemData>();
        if (clueItems.Count() != 0) {
            foreach (var item in clueItems) {
                items.Add(item);
                item.hasBeenPickedUp = false;
                item.pickedUpNumber = 0;
            }
        }
        
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        foreach (var dialogue in dialogueContainers) {
            dialogue.alreadyHadConversation = false;
        }
        
        inventory.newGame();
        this.inventory = inventory;
        

        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.3522396f, 0.2f);
    }
}
