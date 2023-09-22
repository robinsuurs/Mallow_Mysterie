using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Object : MonoBehaviour
{
    // public EventManager _eventManager;
    public GameEvent hit;

    private void OnTriggerEnter(Collider other)
    {
        hit.TriggerEvent();
    }
}
