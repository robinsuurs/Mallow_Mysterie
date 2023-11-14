using System.Collections;
using System.Collections.Generic;
using Dialogue.RunTime;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.Events;

public class DialogueListener : MonoBehaviour
{
    public DialogueEvent Event;
    public UnityEvent<DialogueContainer> response;
    private void OnEnable() {
        Event.AddListener(this);
    }

    private void OnDisable()
    {
        Event.RemoveListener(this);
    }

    public void OnEventTriggered(DialogueContainer dialogue) {
        response.Invoke(dialogue);
    }
}
