using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/ItemDataEvent")]
public class ItemDataEvent : ScriptableObject
{
    private List<ItemDataListernerEvent> _listeners = new List<ItemDataListernerEvent>();
    
    public void Raise(ItemData itemData)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(itemData);
        }
    }
    public void AddListener(ItemDataListernerEvent listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(ItemDataListernerEvent listener)
    {
        _listeners.Remove(listener);
    }
}
