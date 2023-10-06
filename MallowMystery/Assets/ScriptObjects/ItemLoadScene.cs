using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using Unity.VisualScripting;
using UnityEngine;

public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData ItemData;

    public void ShowObjectOrNot() {
        if (ItemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void PickUpObject() {
        ItemData.hasBeenPickedUp = true;
        this.GameObject().SetActive(false);
        Debug.Log("test");
    }
}
