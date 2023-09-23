using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();
    public void Raise()
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }
    public void AddListener(GameEventListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }
}
