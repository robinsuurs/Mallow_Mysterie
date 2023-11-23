using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerEvent : ScriptableObject
{
    private readonly List<SetQuestions> _listeners = new List<SetQuestions>();
    public void Raise()
    {
        for (int i = _listeners.Count -1; i >= 0; i--)
        {
            _listeners[i].OnEventTriggered();
        }
    }
    public void AddListener(SetQuestions listener)
    {
        _listeners.Add(listener);
    }
    public void RemoveListener(SetQuestions listener)
    {
        _listeners.Remove(listener);
    }
}
