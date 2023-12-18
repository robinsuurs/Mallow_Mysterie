using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UIControls : MonoBehaviour
{
    [FormerlySerializedAs("inpAsset")] [SerializeField] private InputActionAsset input;
    private InputAction OInventoryAction;
    private InputAction OSettingsAction;
    private InputAction OMapAction;
    private InputAction ODeductionAction;
    private InputAction moveAction;
    private InputAction interactAction;
    [SerializeField] private newCanvasManager Manager;

    private void Awake()
    {
        OInventoryAction = input.FindAction("OpenInventory");
        OSettingsAction = input.FindAction("OpenSettings");
        OMapAction = input.FindAction("OpenMap");
        ODeductionAction = input.FindAction("openDeduction");
        
        moveAction = input.FindAction("Move");
        interactAction = input.FindAction("Interact");
    }

    private void OnEnable()
    {
        input.Enable();
        OInventoryAction.performed += openJournalClues;
        OSettingsAction.performed += openJournalSettings;
        OMapAction.performed += openJournalMap;
        ODeductionAction.performed += openJournalDeduction;
    }

    private void OnDisable()
    {
        OInventoryAction.performed -= openJournalClues;
        OSettingsAction.performed -= openJournalSettings;
        OMapAction.performed -= openJournalDeduction;
        ODeductionAction.performed -= openJournalMap;
        input.Disable();
    }

    void openJournalClues(InputAction.CallbackContext context) { Manager.openJournalPage(UIPage.Clues); } 
    void openJournalSettings(InputAction.CallbackContext context) { Manager.openJournalPage(UIPage.Settings); }
    void openJournalDeduction(InputAction.CallbackContext context) { Manager.openJournalPage(UIPage.Deduction); }
    void openJournalMap(InputAction.CallbackContext context){ Manager.openJournalPage(UIPage.Map); }
    
    public void activateInput() {moveAction.Enable(); interactAction.Enable();}
    public void deactivateInput() {moveAction.Disable(); interactAction.Disable();}
}
