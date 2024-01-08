using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    
    public class ItemData : ScriptableObject, IDataPersistence
    {
        public string itemName;
        public Sprite icon;
        public string locationFound;
        [Tooltip("Description of the item")] 
        public string description;
        public bool hasBeenPickedUp = false;
        public int pickedUpNumber;
        [SerializeField] private List<PickupEvent> _event;

        public void setPickUp() {
            hasBeenPickedUp = true;
            foreach (var pickUp in _event) {
                pickUp.Raise();
            }
        }
        
        public void LoadData(GameData data) {
            foreach (var dataSave in data.itemDataSaves.Where(dataSave => itemName.Equals(dataSave.itemName))) {
                if (dataSave.hasBeenPickedUp) {
                    setPickUp();
                }
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
