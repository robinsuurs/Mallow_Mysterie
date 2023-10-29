using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        GameObject.FindWithTag("ItemPopUp").gameObject.GetComponent<ItemPopUpManager>().showPopUp(itemData);
        this.GameObject().SetActive(false);
    }
}
