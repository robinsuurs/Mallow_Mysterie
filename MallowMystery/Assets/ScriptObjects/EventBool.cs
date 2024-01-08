using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/BoolEvent")]
public class EventBool : ScriptableObject
{
    private readonly List<EventBoolListener> _listeners = new List<EventBoolListener>();
    
    public void Raise(bool value)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(value);
        }
    }
    public void AddListener(EventBoolListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(EventBoolListener listener)
    {
        _listeners.Remove(listener);
    }
}
