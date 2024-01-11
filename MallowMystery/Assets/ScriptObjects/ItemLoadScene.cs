using System.Collections;
using System.Collections.Generic;
using ScriptObjects;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemDataEvent itemDataEvent;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void InteractWithItem() {
        if (itemData.hasBeenPickedUp) return;
        
        itemDataEvent.Raise(itemData);
        gameObject.SetActive(false);
    }
}
