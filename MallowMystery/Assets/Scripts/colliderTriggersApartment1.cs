using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTriggersApartment1 : MonoBehaviour {
    [SerializeField] private int LayerMaskName;
    private int triggers;

    private void Start() {
        triggers = 0;
    }

    private void OnTriggerEnter(Collider other) {
        triggers += 1;
        ShowRoom();
    }

    private void OnTriggerExit(Collider other)
    {
        triggers -= 1;
        HideRoom();
    }

    private void ShowRoom() {
        if (triggers >= 1) {
            Camera.main.cullingMask |= (1 << LayerMaskName);
        }
    }
    
    private void HideRoom() {
        if (triggers == 0) {
            Camera.main.cullingMask &= ~(1 << LayerMaskName);
        }
    }
}
