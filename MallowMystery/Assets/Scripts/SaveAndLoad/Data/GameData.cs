using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {
    public List<ItemData> items;
    public Inventory Inventory;
    public Scene Scene;
    public Vector3 playerLocation;

    public GameData(Inventory inventory) {
        // this.Inventory = inventory;
        
        this.items = new List<ItemData>();
        var clueItems = Resources.FindObjectsOfTypeAll<ScriptableObject>().OfType<ItemData>();
        if (clueItems.Count() != 0) {
            foreach (var item in clueItems) {
                items.Add(item);
            }
        }

        this.Inventory = inventory;
        Inventory.items = items;
        //Set start thing when you create a newGame
        // Scene = startscene;
        //playerLocation = StartLocation of player
    }
}
