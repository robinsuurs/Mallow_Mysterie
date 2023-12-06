using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBoolListener : MonoBehaviour
{
    public EventBool Event;
    public UnityEvent<bool> response;
        

    private void OnEnable()
    {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(bool value)
    {
        response.Invoke(value);
    }
}
