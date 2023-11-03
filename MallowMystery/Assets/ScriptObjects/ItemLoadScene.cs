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
    [SerializeField] private List<ItemData> itemDatas;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private bool removeAfterPickUp;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp || (itemDatas.Count != 0 && itemDatas[0].hasBeenPickedUp && removeAfterPickUp)) {
            this.GameObject().SetActive(false);
        }
    }

    public void PickUpObject() {
        if (itemData == null) {
            for (int i = 0; i < itemDatas.Count; i++) {
                if (!itemDatas[i].hasBeenPickedUp) {
                    itemDatas[i].hasBeenPickedUp = true;
                    itemDatas[i].pickedUpNumber = _inventory.pickedUpItemNumber();
                    GameObject.FindWithTag("ItemPopUp").gameObject.GetComponent<ItemPopUpManager>().showPopUp(itemDatas[i]);
                    if (itemDatas.Count - 1 == i) {
                        if (removeAfterPickUp) {
                            this.GameObject().SetActive(false);
                        }
                        else if (itemDatas[itemDatas.Count - 1].hasBeenPickedUp) {
                            GetComponent<GameEventListeners>().enabled = false;
                        }
                    }
                    break;
                }
            }
        } else {
            itemData.hasBeenPickedUp = true;
            itemData.pickedUpNumber = _inventory.pickedUpItemNumber();
            GameObject.FindWithTag("ItemPopUp").gameObject.GetComponent<ItemPopUpManager>().showPopUp(itemData);
            this.GameObject().SetActive(false);
        }
        
    }
}
