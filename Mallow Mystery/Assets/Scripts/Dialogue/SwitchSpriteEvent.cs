using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/SwitchSpriteEvent")]
public class SwitchSpriteEvent : ScriptableObject
{
    private List<SwitchSpriteListener> _listeners = new List<SwitchSpriteListener>();
    
    public void Raise(string speakerLeft, string speakerRight)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(speakerLeft, speakerRight);
        }
    }
    public void AddListener(SwitchSpriteListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(SwitchSpriteListener listener)
    {
        _listeners.Remove(listener);
    }
}
