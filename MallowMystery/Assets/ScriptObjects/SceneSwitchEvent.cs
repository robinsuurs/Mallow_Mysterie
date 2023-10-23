using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneSwitch/SwitchEvent")]
public class SceneSwitchEvent : ScriptableObject
{
    private List<SceneSwitchListener> _listeners = new List<SceneSwitchListener>();
    
    public void Raise(SceneSwitchData sceneSwitchData)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(sceneSwitchData);
        }
    }
    public void AddListener(SceneSwitchListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(SceneSwitchListener listener)
    {
        _listeners.Remove(listener);
    }
}
