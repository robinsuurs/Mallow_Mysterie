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
    private List<ItemData> pickedUpItems;
    
    [SerializeField] private List<GameObject> inventoryPlaceholders;
    [SerializeField] private GameObject selectedImageLoc;

    public void setInventoryItems() {
        pageNumber = 0;
        pickedUpItems = Resources.LoadAll("Clues/ClueInventory", typeof(Inventory))
            .Cast<Inventory>().FirstOrDefault(inventoryArray => inventoryArray.name.Equals("ClueInventory"))
            ?.items.Where(itemData => itemData.hasBeenPickedUp).OrderBy(data => data.pickedUpNumber).ToList();
        setPage();
    }

    public void flipPageForward() {
        if ((pageNumber + 1) * 6 > pickedUpItems.Count) {
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
        for (int i = 0; i < 6; i++) {
            if (i + pageNumber * 6 < pickedUpItems.Count) {
                inventoryPlaceholders[i].transform.Find("Image").gameObject.GetComponent<Image>().sprite = pickedUpItems[i + pageNumber * 6].icon;
                inventoryPlaceholders[i].transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = pickedUpItems[i + pageNumber * 6].itemName;
                inventoryPlaceholders[i].GetComponent<Button>().enabled = true;
            } else {
                inventoryPlaceholders[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    public void itemCloseUp(int clickedItemNumber) {
        selectedImageLoc.transform.Find("ItemImage").GetComponent<Image>().sprite = pickedUpItems[clickedItemNumber + pageNumber * 6].icon;
        selectedImageLoc.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = pickedUpItems[clickedItemNumber + pageNumber * 6].itemName;
        selectedImageLoc.transform.Find("ItemLocationFound").GetComponent<TextMeshProUGUI>().text = pickedUpItems[clickedItemNumber + pageNumber * 6].description;
    }
}
