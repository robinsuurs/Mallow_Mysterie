using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisableMainMenuInputs : MonoBehaviour {
    [SerializeField] private InputActionAsset actionAsset;
    private void OnEnable() {
        actionAsset.Disable();
    }

    private void OnDestroy() {
        actionAsset.Enable();
    }
}
