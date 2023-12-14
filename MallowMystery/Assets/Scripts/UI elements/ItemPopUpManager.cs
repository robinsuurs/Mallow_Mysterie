using System;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemPopUpManager : MonoBehaviour {
    [SerializeField] private GameObject ItemPopUpScreen;
    [SerializeField] private InputActionAsset _inputAction;
    private ItemData _itemData;
        
    public void showPopUp(ItemData itemData) {
        Time.timeScale = 0;
        this._itemData = itemData;
        GameObject itemHolder = ItemPopUpScreen.transform.Find("ItemHolder").gameObject;
        itemHolder.transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite = itemData.icon;
        itemHolder.transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = itemData.itemName;
        ItemPopUpScreen.SetActive(true);
        _inputAction.Disable();
    }

    public void closePopUp() {
        ItemPopUpScreen.SetActive(false);
        Time.timeScale = 1;
        _inputAction.Enable();
    }

    public void seeInInventory() {
        closePopUp();
        GameObject.FindWithTag("CanvasManager").gameObject.transform.Find("SettingsScreen").gameObject.GetComponent<SettingsScreenManager>().showSettingsScreen("Inventory");
        GameObject.FindWithTag("CanvasManager").gameObject.transform.Find("SettingsScreen").gameObject.transform.Find("MenuContainer").transform.Find("Inventory").GetComponent<InventoryScreen>().newItemPickUp(_itemData);
    }

    private void Update() {
        if (this.isActiveAndEnabled && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))) {
            closePopUp();
        }
    }
}