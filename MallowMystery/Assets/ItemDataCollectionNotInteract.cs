using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;

public class ItemDataCollectionNotInteract : MonoBehaviour {
    [SerializeField] private List<ItemData> containedItems;

    public void setInteraction() {
        if (containedItems.All(clue => clue.hasBeenPickedUp)) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<GameEventListeners>().enabled = false;
        }
    }
}
