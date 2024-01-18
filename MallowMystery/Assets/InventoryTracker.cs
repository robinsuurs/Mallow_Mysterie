using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

public class InventoryTracker : MonoBehaviour {
    [SerializeField] private Inventory _inventory;

    public void PickUpItem(ItemData itemData) {
        if (itemData.hasBeenPickedUp) return;
        
        itemData.setPickUp();
        itemData.pickedUpNumber = _inventory.PickedUpItemNumber();
    }
}
