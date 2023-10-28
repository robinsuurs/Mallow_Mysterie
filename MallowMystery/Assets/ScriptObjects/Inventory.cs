using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    public class Inventory : ListOfStuff<ItemData>, IDataPersistence {

        public void newGame(List<ItemData> items) {
            this.items = items;
        }
        
        public int pickedUpItemNumber() {
            return this.items.Count(item => item.hasBeenPickedUp);
        }
        
        public void LoadData(GameData data) {
            foreach (var item in data.inventory.items) {
                foreach (var dataSave in data.itemDataSaves.Where(dataSave => item.itemName.Equals(dataSave.itemName))) {
                    item.hasBeenPickedUp = dataSave.hasBeenPickedUp;
                    item.pickedUpNumber = dataSave.pickedUpNumber;
                    break;
                }
            }
        }

        public void SaveData(ref GameData data) {
            data.itemDataSaves.Clear();
            foreach (var itemDataSave in data.inventory.items.Select(item => new ItemDataSave(item.itemName, item.hasBeenPickedUp, item.pickedUpNumber))) {
                data.itemDataSaves.Add(itemDataSave);
            }
        }
    }
}
