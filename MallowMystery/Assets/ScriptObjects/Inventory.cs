using System.Collections;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    public class Inventory : ListOfStuff<ItemData>, IDataPersistence {
        public void LoadData(GameData data) {
            this.items = data.items;
        }

        public void SaveData(ref GameData data) {
            data.items = this.items;
        }
    }
}
