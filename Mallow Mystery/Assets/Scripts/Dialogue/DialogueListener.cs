using System.Collections;
using System.Collections.Generic;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.Events;

public class DialogueListener : MonoBehaviour
{
    // Start is called before the first frame update
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
