using System.Collections;
using System.Collections.Generic;
using ScriptObjects;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class ItemLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemDataEvent itemDataEvent;
    [SerializeField] private bool dontRemoveObject;
    
    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            CheckShowObject();
        }
    }

    public void InteractWithItem() {
        if (itemData.hasBeenPickedUp) return;
        
        itemDataEvent.Raise(itemData);
        CheckShowObject();
    }

    private void CheckShowObject() {
        if (dontRemoveObject) {
            gameObject.GetComponent<GameEventListeners>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        } else {
            gameObject.SetActive(false);
        }
    }
}
