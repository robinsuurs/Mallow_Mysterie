using System;
using System.Collections;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemPopUpManager : MonoBehaviour {
    [SerializeField] private GameObject ItemPopUpScreen;
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI itemShortDescription;
    [SerializeField] private InputActionAsset _inputAction;
    
    [SerializeField] private FadeToBlackEvent fadeToBlackEvent;
    
    [SerializeField] private UnityEvent openUI;
    [SerializeField] private UnityEvent<ItemData> showInv;
    private ItemData _itemData;
        
    public void showPopUp(ItemData itemData) {
        this._itemData = itemData;
        ItemImage.sprite = itemData.icon;
        ItemName.text = itemData.itemName;
        itemShortDescription.text = itemData.shortDescription;
        fadeToBlackEvent.Raise();
        ItemPopUpScreen.SetActive(true);
        _inputAction.Disable();
    }

    public void closePopUp() {
        ItemPopUpScreen.SetActive(false);
        _inputAction.Enable();
    }
    
    public void seeInInventory() {
        closePopUp();
        openUI.Invoke();
        showInv.Invoke(_itemData);
    }

    private void Update() {
        if (ItemPopUpScreen.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))) {
            closePopUp();
        }
    }
}