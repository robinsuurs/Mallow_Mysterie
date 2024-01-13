using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class ItemDataListernerEvent : MonoBehaviour
{
    public ItemDataEvent Event;
    public UnityEvent<ItemData> response;
        

    private void OnEnable()
    {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(ItemData value)
    {
        response.Invoke(value);
    }
}
