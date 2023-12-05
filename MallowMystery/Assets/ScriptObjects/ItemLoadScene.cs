using System.Collections;
using System.Collections.Generic;
using ScriptObjects;

using Unity.VisualScripting;
using UnityEngine;


public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    [SerializeField] private Inventory _inventory;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void PickUpObject() {
        if (GameObject.Find("ItemPopUp") == null || !GameObject.Find("ItemPopUp").activeSelf) {
            itemData.setPickUp();
            itemData.pickedUpNumber = _inventory.pickedUpItemNumber();
            GameObject.FindWithTag("ItemPopUp").gameObject.GetComponent<ItemPopUpManager>().showPopUp(itemData);
            this.GameObject().SetActive(false);
        }
    }
}
