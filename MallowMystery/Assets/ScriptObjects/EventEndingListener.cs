using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventEndingListener : MonoBehaviour
{
    public EndingEvent Event;
    public UnityEvent<EndingStringList> response;


    private void OnEnable()
    {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(EndingStringList endingStringList)
    {
        response.Invoke(endingStringList);
    }
}
