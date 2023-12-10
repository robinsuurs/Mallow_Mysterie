using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/EndingEvent")]
public class EndingEvent : ScriptableObject
{
    private List<EventEndingListener> _listeners = new List<EventEndingListener>();
    
    public void Raise(EndingStringList endingStringList)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(endingStringList);
        }
    }
    public void AddListener(EventEndingListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(EventEndingListener listener)
    {
        _listeners.Remove(listener);
    }
}
