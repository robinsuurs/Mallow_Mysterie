using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void PickUpObject() {
        itemData.hasBeenPickedUp = true;
        this.GameObject().SetActive(false);
    }
}
