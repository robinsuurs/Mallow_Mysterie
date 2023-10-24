using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class SceneSwitchListener : MonoBehaviour
{
    public SceneSwitchEvent Event;
    public UnityEvent<SceneSwitchData> response;

    private void OnEnable() {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(SceneSwitchData sceneSwitchData)
    {
        response.Invoke(sceneSwitchData);
    }
}
