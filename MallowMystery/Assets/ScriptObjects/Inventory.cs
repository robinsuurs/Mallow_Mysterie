using System.Collections;
using System.Linq;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    public class Inventory : ListOfStuff<ItemData>, IDataPersistence {
        [SerializeField] private int itemsPickedUp = 0;

        public void newGame() {
            itemsPickedUp = 0;
        }
        
        public int pickedUpItemNumber() {
            itemsPickedUp++;
            return itemsPickedUp;
        }
        
        public void LoadData(GameData data) {
            this.items = data.items;
            this.itemsPickedUp = data.inventory.itemsPickedUp;
            foreach (var item in data.inventory.items) {
                foreach (var dataSave in data.ItemDataSaves.Where(dataSave => item.itemName.Equals(dataSave.itemName))) {
                    item.hasBeenPickedUp = dataSave.hasBeenPickedUp;
                    item.pickedUpNumber = dataSave.pickedUpNumber;
                    break;
                }
            }
        }

        public void SaveData(ref GameData data) {
            data.ItemDataSaves.Clear();
            data.items = this.items;
            data.inventory.itemsPickedUp = this.itemsPickedUp;
            foreach (var itemDataSave in data.inventory.items.Select(item => new ItemDataSave(item.itemName, item.hasBeenPickedUp, item.pickedUpNumber))) {
                data.ItemDataSaves.Add(itemDataSave);
            }
        }
    }
}
