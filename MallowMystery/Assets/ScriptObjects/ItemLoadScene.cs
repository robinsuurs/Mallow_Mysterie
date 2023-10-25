using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    [SerializeField] private Inventory _inventory;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void PickUpObject() {
        itemData.hasBeenPickedUp = true;
        itemData.pickedUpNumber = _inventory.pickedUpItemNumber();
        this.GameObject().SetActive(false);
    }
}
