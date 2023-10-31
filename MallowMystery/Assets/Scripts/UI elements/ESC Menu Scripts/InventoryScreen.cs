using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour {

    [SerializeField] private List<GameObject> inventoryPlaceholders;

    public void setInventoryItems() {
        List<ItemData> pickedUpItems = Resources.LoadAll("Clues/ClueInventory", typeof(Inventory))
            .Cast<Inventory>().FirstOrDefault(inventoryArray => inventoryArray.name.Equals("ClueInventory"))
            ?.items.Where(itemData => itemData.hasBeenPickedUp).OrderBy(data => data.pickedUpNumber).ToList();
        for (int i = 0; i < pickedUpItems.Count; i++) {
            // inventoryPlaceholders[i].
            inventoryPlaceholders[i].transform.Find("ItemImage").gameObject.GetComponent<Image>().sprite =
                pickedUpItems[i].icon;
            inventoryPlaceholders[i].transform.Find("ItemName").gameObject.GetComponent<TextMeshProUGUI>().text = pickedUpItems[i].itemName;
            inventoryPlaceholders[i].transform.Find("ItemDescription").gameObject.GetComponent<TextMeshProUGUI>().text = pickedUpItems[i].description;
        }
    }
}
