using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/UIEvent")]
public class UIEvent : ScriptableObject
{
    private List<UIEventListener> _listeners = new List<UIEventListener>();

    public void Raise(UIPage page)
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered(page);
        }
    }
    public void AddListener(UIEventListener listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(UIEventListener listener)
    {
        _listeners.Remove(listener);
    }
    
    public void RaiseClues() => Raise(UIPage.Clues);
    public void RaiseDeduction() => Raise(UIPage.Deduction);
    public void RaiseSettings() => Raise(UIPage.Settings);
    public void RaiseMap() => Raise(UIPage.Map);
    public void RaiseFolder() => Raise(UIPage.Folder);
    public void RaisePinboard() => Raise(UIPage.Pinboard);
}
