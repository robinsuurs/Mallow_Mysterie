using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CollisionTrigger : MonoBehaviour
{
    public GameEventChannel gameEventChannel;
    private void OnTriggerEnter(Collider other)
    {
        gameEventChannel.Raise();
    }
}
