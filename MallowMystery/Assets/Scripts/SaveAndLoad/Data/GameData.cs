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
    public Inventory Inventory;
    public string SceneName;
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
            }
        }
        
        List<DialogueContainer> dialogueContainers = Resources.LoadAll<DialogueContainer>("").ToList();
        foreach (var dialogue in dialogueContainers) {
            dialogue.alreadyHadConversation = false;
        }

        this.Inventory = inventory;

        SceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.5f, 0.2f);
    }
}
