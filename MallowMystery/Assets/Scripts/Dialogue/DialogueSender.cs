using System.Collections;
using System.Collections.Generic;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

public class DialogueSender : MonoBehaviour {
    private DialogueContainer _dialogueContainer;
    public void sendDialogue() {
        if (_dialogueContainer != null) {
            GameObject.Find("DialogueHandlerObject").transform.GetComponent<DialogueHandler>().StartDialogue(_dialogueContainer);
        } else {
            Debug.Log("No dialogue set to load.");
        }
    }

    public void setDialogueContainer(DialogueContainer dialogueContainer) {
        this._dialogueContainer = dialogueContainer;
    }
}
