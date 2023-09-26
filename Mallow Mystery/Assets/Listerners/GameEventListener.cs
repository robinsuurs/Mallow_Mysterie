using System.Collections;
using System.Collections.Generic;
using ExampleEventScriptAble;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameEventListeners : MonoBehaviour
{
    public GameEventChannel gameEventChannel;
    void OnEnable()
    {
        gameEventChannel.AddListener(this);
    }
    
    void OnDisable()
    {
        gameEventChannel.RemoveListener(this);
    }
    
    public void OnEventTriggered()
    {
        //Put things that need to be done here
    }
}
