using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using UnityEngine;
using UnityEngine.Events;

public class ListernerStandardAdd : MonoBehaviour
{

    public GameEventStandardAdd Event;
    public UnityEvent response;


    private void OnEnable()
    {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered()
    {
        response.Invoke();
    }
}
