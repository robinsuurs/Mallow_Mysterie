using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemDataSave
{
    public string itemName;
    public Sprite icon;
    [Tooltip("Description of the item")] 
    public string description;
    public bool hasBeenPickedUp = false;
    public int pickedUpNumber;

    public ItemDataSave(string itemName, bool hasBeenPickedUp, int pickedUpNumber) {
        this.itemName = itemName;
        this.hasBeenPickedUp = hasBeenPickedUp;
        this.pickedUpNumber = pickedUpNumber;
    }
}
