using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {
    public List<ItemData> items;
    public Scene Scene;
    public Vector3 playerLocation;
    public SerliazableDictionary<string, bool> cluesPickedUp; //Perhaps not needed

    public GameData(Inventory inventory) {
        foreach (var item in inventory.items) {
            item.hasBeenPickedUp = false;
        }
        //Set start thing when you create a newGame
        cluesPickedUp = new SerliazableDictionary<string, bool>();
        //Scene = startscene
        //playerLocation = StartLocation of player
    }
}
