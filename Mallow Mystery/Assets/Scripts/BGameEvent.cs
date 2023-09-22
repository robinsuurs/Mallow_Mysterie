using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "BGameEvent")]
public class BGameEvent : ScriptableObject
{
    public List<BGameEventListener> listeners = new List<BGameEventListener>();

    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(BGameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    
    public void UnRegisterListener(BGameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
