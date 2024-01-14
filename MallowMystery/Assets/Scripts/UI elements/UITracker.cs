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
        CheckAmount();
    }

    public void LowerAmount() {
        UIAmountTracker--;
        CheckAmount();
    }
    
    private void CheckAmount() {
        switch (UIAmountTracker) {
            case <= 0:
                playerInput.Enable();
                fadeToNormal.Raise();
                if (UIAmountTracker < 0) {
                    Debug.LogWarning("tracking UI below 0 how?");
                }
                break;
            default:
                playerInput.Disable();
                fadeToBlack.Raise();
                break;
        }
    }
    
}
