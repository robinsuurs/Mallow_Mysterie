using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class ItemDataNamesRetriever
{
    public string ClueItems = "Clues/BramTest";
    public string[] itemDatasTest;
    public List<string> itemDataNames;

    public ItemDataNamesRetriever() {
        RetrieveItemDataNames();
    }

    public void RetrieveItemDataNames() {
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

        List<string> namesOfItems = new List<string>();
        
        for (int i = 0; i < newItemData.Length; i++) {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            newItemData[i] = AssetDatabase.LoadAssetAtPath<Object>(path);
            string itemName = GetFieldValue<string>(newItemData[i], "itemName");
            namesOfItems.Add(itemName);
        }

        this.itemDataNames = namesOfItems;
    }
    
    //https://stackoverflow.com/questions/33684027/getting-property-and-field-values-from-an-object-using-reflection-and-using-a-st
    public System.Object GetFieldValue( System.Object obj, String name)
    {
        foreach (String part in name.Split('.'))
        {
            if (obj == null) { return null; }

            Type type = obj.GetType();
            FieldInfo info = type.GetField(part);
            if (info == null) { return null; }

            obj = info.GetValue(obj);
        }
        return obj;
    }

// so we can use reflection to access the object properties
    public T GetFieldValue<T>( Object obj, String name)
    {
        System.Object retval = GetFieldValue(obj, name);
        if (retval == null) { return default(T); }

        // throws InvalidCastException if types are incompatible
        return (T)retval;
    }
    
    
}
