using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData {
    public List<ItemData> items;
    public Scene Scene;
    public Vector3 playerLocation;
    public int test;
    public SerliazableDictionary<string, bool> cluesPickedUp;

    public GameData () {
        //Set start thing when you create a newGame
        cluesPickedUp = new SerliazableDictionary<string, bool>();
        //Scene = startscene
    }
}
