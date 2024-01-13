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
    
    [SerializeField] private UnityEvent openUI;
    [SerializeField] private GameEventStandardAdd openUIElement;
    [SerializeField] private GameEventStandardAdd closeUIElement;
    [SerializeField] private EventSound pickUpSound;
    [SerializeField] private AudioClip pickUpSoundAudio;
    [SerializeField] private UnityEvent<ItemData> showInv;
    private ItemData _itemData;
        
    public void showPopUp(ItemData itemData) {
        if (ItemPopUpScreen.activeSelf) {
            closePopUp();
        }
        _itemData = itemData;
        ItemImage.sprite = itemData.icon;
        ItemName.text = itemData.itemName;
        itemShortDescription.text = itemData.shortDescription;
        openUIElement.Raise();
        pickUpSound.Raise(pickUpSoundAudio);
        ItemPopUpScreen.SetActive(true);
    }

    public void closePopUp() {
        ItemPopUpScreen.SetActive(false);
        closeUIElement.Raise();
    }
    
    public void seeInInventory() {
        openUI.Invoke();
        showInv.Invoke(_itemData);
        closePopUp();
    }

    private void Update() {
        if (ItemPopUpScreen.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))) {
            closePopUp();
        }
    }
}