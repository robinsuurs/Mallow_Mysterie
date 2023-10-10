using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class TestScript
{
    public string ClueItems = "Clues/BramTest";
    public string[] itemDatasTest;

    public void TestingShite() {
        string fullPath = $"{Application.dataPath}/{ClueItems}";
        if (!System.IO.Directory.Exists(fullPath)) {            
            return;
        }

        var folders = new string[]{$"Assets/{ClueItems}"};
        var guids = AssetDatabase.FindAssets("t:ItemData", folders);

        var newItemData = new Object[guids.Length];

        bool mismatch;
        if (itemDatasTest == null) {
            mismatch = true;
        } else {
            mismatch = newItemData.Length != itemDatasTest.Length;
        }

        Object[] testing = AssetDatabase.LoadAllAssetsAtPath($"{Application.dataPath}/{ClueItems}");

        for (int i = 0; i < newItemData.Length; i++) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newItemData[i] = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            ItemData test2 = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            // ItemData2 objectTest = AssetDatabase.LoadAssetAtPath<ItemData2>(path);
            ItemData test3 = (ItemData)newItemData[i];
            string test99 = test3.itemName;
            bool test909 = test3.hasBeenPickedUp;
            string test = test99;
        }
    }
    
    
}
