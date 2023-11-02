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
        test();
        Debug.Log("walkIn");
    }

    private void OnTriggerExit(Collider other)
    {
        triggers -= 1;
        test2();
        Debug.Log("walkOut");
    }

    private void test() {
        if (triggers >= 1) {
            Camera.main.cullingMask |= (1 << LayerMaskName);
        }
        Debug.Log(triggers);
    }
    
    private void test2() {
        if (triggers == 0) {
            Camera.main.cullingMask &= ~(1 << LayerMaskName);
        }
        Debug.Log(triggers);
    }
}
