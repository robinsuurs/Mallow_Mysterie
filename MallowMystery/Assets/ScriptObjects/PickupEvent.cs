using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/AnswerEvent")]
public class PickupEvent : ScriptableObject
{
    private readonly List<Answer> _listeners = new List<Answer>();
    public void Raise()
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }
    public void AddListener(Answer answer)
    {
        _listeners.Add(answer);
    }
    public void RemoveListener(Answer answer)
    {
        _listeners.Remove(answer);
    }
}
