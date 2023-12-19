using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class UIEventListener : MonoBehaviour
{
    public UIEvent Event;
    public UnityEvent<UIPage> response;

    private void OnEnable() {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(UIPage page)
    {
        response.Invoke(page);
    }
}
