using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using UnityEngine;

public class VoidEventListener : MonoBehaviour
{
    public VoidEventChannel voidEventChannel;
    void OnEnable()
    {
        voidEventChannel.AddListener(this);
    }
    
    void OnDisable()
    {
        voidEventChannel.RemoveListener(this);
    }
    
    public void OnEventTriggered()
    {
        Vector3 player = GameObject.Find("Player").transform.position;
        transform.Translate(new Vector3(0, player.y + 3, 0));
    }
}
