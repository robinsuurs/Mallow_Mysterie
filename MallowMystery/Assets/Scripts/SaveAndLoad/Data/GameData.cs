using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Editor.Nodes;
using Dialogue.Runtime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {
    public List<ItemData> items;
    public Inventory Inventory;
    public Scene Scene;
    public Vector3 playerLocation;
    public List<DialogueNodeData> dialogues;

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

        this.Inventory = inventory;
        
        List<DialogueContainer>dialogues = Resources.LoadAll<DialogueContainer>("").ToList();

        foreach (var dialogueNodeData in dialogues.SelectMany(dialogue => dialogue.DialogueNodeData)) {
            dialogueNodeData.alreadyHadConversation = false;
        }

        // Scene = startscene;
        //playerLocation = StartLocation of player
    }
}
