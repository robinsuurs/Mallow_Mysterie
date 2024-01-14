using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class CheckFullInv : MonoBehaviour {
    [SerializeField] private Inventory clueInv;
    [SerializeField] private UnityEvent permissionCheck;

    public void checkFullInv() {
        if (clueInv.items.All(clue => clue.hasBeenPickedUp)) {
            permissionCheck.Invoke();
        }
    }
}
