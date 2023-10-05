using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/GameEventStandardAdd")]
public class GameEventStandardAdd : ScriptableObject
{
    private List<ListernerStandardAdd> _listeners = new List<ListernerStandardAdd>();
    
    public void Raise()
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }
    public void AddListener(ListernerStandardAdd listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(ListernerStandardAdd listener)
    {
        _listeners.Remove(listener);
    }
}

