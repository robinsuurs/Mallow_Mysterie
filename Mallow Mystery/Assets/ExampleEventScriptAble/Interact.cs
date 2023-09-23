using System;
using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public VoidEventChannel voidEventChannel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            voidEventChannel.Raise();
        }
    }
}
