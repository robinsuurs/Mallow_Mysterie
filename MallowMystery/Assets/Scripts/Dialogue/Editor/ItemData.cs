using System;
using UnityEngine;

public class ItemData: ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [Tooltip("Description of the item")] 
    public string description;
    public bool hasBeenPickedUp = false;
        
}
