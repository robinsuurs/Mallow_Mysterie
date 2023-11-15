using System;
using System.Linq;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    
    public class ItemData : ScriptableObject, IDataPersistence
    {
        public string itemName;
        public Sprite icon;
        [Tooltip("Description of the item")] 
        public string description;
        public bool hasBeenPickedUp = false;
        public int pickedUpNumber;
        
        public void LoadData(GameData data) {
            foreach (var dataSave in data.itemDataSaves.Where(dataSave => itemName.Equals(dataSave.itemName))) {
                hasBeenPickedUp = dataSave.hasBeenPickedUp;
                pickedUpNumber = dataSave.pickedUpNumber;
                break;
            }
        }

        public void SaveData(ref GameData data) {
            foreach (var t in data.itemDataSaves.Where(t => t.itemName.Equals(itemName))) {
                t.pickedUpNumber = pickedUpNumber;
                t.hasBeenPickedUp = hasBeenPickedUp;
                return;
            }
            data.itemDataSaves.Add(new ItemDataSave(itemName, hasBeenPickedUp, pickedUpNumber));
        }
    }
}
