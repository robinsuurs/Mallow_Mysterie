using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour {

    private int pageNumber;
    [SerializeField] private Inventory inventory;
    private List<ItemData> pickedUpItems;
    
    [SerializeField] private List<GameObject> inventoryPlaceholders;
    [SerializeField] private GameObject selectedImageLoc;
    [SerializeField] private TextMeshProUGUI currentPage;
    [SerializeField] private Image closeUpImageClue;
    [SerializeField] private TextMeshProUGUI clueName;
    [SerializeField] private TextMeshProUGUI locationClue;
    [SerializeField] private TextMeshProUGUI clueDescription;
    private ItemData currentSelectedItemData;
    [SerializeField] private ItemDataEvent zoomInClue;

    private void Start() {
        itemCloseUp(-1);
    }

    public void setInventoryItems(int pageNumber) {
        this.pageNumber = pageNumber;
        pickedUpItems = inventory?.items.Where(itemData => itemData.hasBeenPickedUp).OrderBy(data => data.pickedUpNumber).ToList();
        setPage();
    }

    public void flipPageForward() {
        if (pickedUpItems.Count > (pageNumber + 1) * 6) {
            pageNumber += 1;
            setPage();
        }
    }

    public void flipPageBackwards() {
        if (pageNumber - 1 >= 0) {
            pageNumber -= 1;
            setPage();
        }
    }

    private void setPage() {
        currentPage.text = (pageNumber + 1).ToString();
        for (int i = 0; i < 6; i++) {
            if (i + pageNumber * 6 < pickedUpItems.Count) {
                inventoryPlaceholders[i].SetActive(true);
                inventoryPlaceholders[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite = pickedUpItems[i + pageNumber * 6].icon;
                inventoryPlaceholders[i].transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = pickedUpItems[i + pageNumber * 6].itemName;
                inventoryPlaceholders[i].GetComponent<Button>().enabled = true;
            } else {
                inventoryPlaceholders[i].SetActive(false);
            }
        }
    }
    
    public void itemCloseUp(int clickedItemNumber) {
        if (clickedItemNumber == -1) {
            closeUpImageClue.sprite = null;
            clueDescription.text = "";
            clueName.text = "";
            locationClue.text = "";
            currentSelectedItemData = null;
        } else {
            int itemNumber = clickedItemNumber + pageNumber * 6;
            closeUpImageClue.sprite = pickedUpItems[itemNumber].icon;
            clueName.text = pickedUpItems[itemNumber].itemName;
            clueDescription.text = pickedUpItems[itemNumber].description;
            locationClue.text = pickedUpItems[itemNumber].locationFound;
            currentSelectedItemData = pickedUpItems[itemNumber];
        }
    }

    public void ZoomInClueSprite() {
        if (currentSelectedItemData != null) {
            zoomInClue.Raise(currentSelectedItemData);
        }
    }

    public void newItemPickUp(ItemData pickedUpItem) {
        int pageNumber = pickedUpItem.pickedUpNumber != 1 ? (pickedUpItem.pickedUpNumber - 1) / 6 : 0;
        setInventoryItems(pageNumber);
        itemCloseUp(pickedUpItem.pickedUpNumber - 1 - pageNumber * 6);
    }
}
