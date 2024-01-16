using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITracker : MonoBehaviour {
    [SerializeField] private int UIAmountTracker = 0;
    [SerializeField] private FadeToBlackEvent fadeToNormal;
    [SerializeField] private FadeToBlackEvent fadeToBlack;
    [SerializeField] private GameEventStandardAdd closedLastUI;
    [SerializeField] private List<InputActionReference> disables;

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
                foreach (var disable in disables) {
                    disable.action.Enable();
                }
                fadeToNormal.Raise();
                closedLastUI.Raise();
                if (UIAmountTracker < 0) {
                    Debug.LogWarning("tracking UI below 0 how?");
                }
                break;
            default:
                foreach (var disable in disables) {
                    disable.action.Disable();
                }
                fadeToBlack.Raise();
                break;
        }
    }
    
}
