using System.Collections.Generic;
using UnityEngine;

namespace ScriptObjects
{
    public abstract class ListOfStuff<T> : ScriptableObject, IDataPersistence
    {
        public List<T> items = new List<T>();

        public void Add(T thing)
        {
            if (!items.Contains(thing))
                items.Add(thing);
        }

        public void Remove(T thing)
        {
            if (items.Contains(thing))
                items.Remove(thing);
        }

        public void LoadData(GameData data) {
            throw new System.NotImplementedException();
        }

        public void SaveData(ref GameData data) {
            throw new System.NotImplementedException();
        }
    }
}

