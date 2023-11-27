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
    public List<DropdownInfo> dropdownAnswers = new List<DropdownInfo>();

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
        
        questionAnswerDic.Clear();
        
        sceneName = "DetectiveRoom";
        playerLocation = new Vector3(-0.5f, 0.2433f, 0.2f);
    }

    public void setDropdownInfo(string nameDropDown, string value) {
        foreach (var dropdown in dropdownAnswers.Where(dropdown => dropdown.nameDropdown.Equals(nameDropDown))) {
            dropdown.value = value;
            return;
        }

        dropdownAnswers.Add(new DropdownInfo(nameDropDown, value));
    }
}
