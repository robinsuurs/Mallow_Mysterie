using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITracker : MonoBehaviour {
    [SerializeField] private int UIAmountTracker = 0;
    [SerializeField] private FadeToBlackEvent fadeToNormal;
    [SerializeField] private FadeToBlackEvent fadeToBlack;
    [SerializeField] private InputActionAsset playerInput;

    public void RaiseAmount() {
        UIAmountTracker++;
        playerInput.Disable();
        fadeToBlack.Raise();
    }

    public void LowerAmount() {
        UIAmountTracker--;
        switch (UIAmountTracker) {
            case < 0:
                Debug.LogWarning("tracking UI below 0 how?");
                break;
            case > 0:
                return;
        }

        playerInput.Enable();
        fadeToNormal.Raise();
    }
    
}
