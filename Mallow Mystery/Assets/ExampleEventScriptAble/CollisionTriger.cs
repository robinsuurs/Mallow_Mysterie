using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public VoidEventChannel voidEventChannel;
    private void OnTriggerEnter(Collider other)
    {
        voidEventChannel.Raise();
    }
}
