using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    public class Inventory : ListOfStuff<ItemData> {
        public int pickedUpItemNumber() {
            return this.items.Count(item => item.hasBeenPickedUp);
        }
    }
}
