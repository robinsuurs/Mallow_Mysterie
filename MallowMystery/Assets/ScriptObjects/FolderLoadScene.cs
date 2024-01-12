using System.Collections;
using System.Collections.Generic;
using ScriptObjects;

using Unity.VisualScripting;
using UnityEngine;


public class FolderLoadScene : MonoBehaviour {
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemDataEvent itemDataEvent;

    public void ShowObjectOrNot() {
        if (itemData.hasBeenPickedUp) {
            this.GameObject().SetActive(false);
        }
    }

    public void InteractWithItem() {
        // if (itemData.hasBeenPickedUp) return;
        
        itemDataEvent.Raise(itemData);
        gameObject.SetActive(false);
    }
}
