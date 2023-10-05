using System;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu]
    
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [Tooltip("Description of the item")] 
        public string description;
        public bool hasBeenPickedUp;

       public void Pickup(bool set)
        {
            hasBeenPickedUp = set;
        }
    }
}
