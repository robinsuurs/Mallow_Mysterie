using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Events;

public class FadeToBlackListener : MonoBehaviour
{
    public List<FadeToBlackEvent> events;
    public UnityEvent<float, float, bool> response;
        

    private void OnEnable()
    {
        foreach (var Event in events) {
            Event.AddListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (var Event in events) {
            Event.RemoveListener(this);
        }
    }

    public void OnEventTriggered(float fadespeed, float opacity, bool goToEndScreen)
    {
        response.Invoke(fadespeed, opacity, goToEndScreen);
    }
}
