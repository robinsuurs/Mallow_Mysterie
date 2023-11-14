using System.Collections;
using System.Collections.Generic;
using Dialogue.RunTime;
using UnityEngine;
using UnityEngine.Events;

public class DialogueCheck : MonoBehaviour {
    [SerializeField] private DialogueContainer DialogueContainer;
    public UnityEvent UnityEvent;

    public void checkIfConversationHad() {
        if (DialogueContainer.alreadyHadConversation) {
            UnityEvent.Invoke();
        }
    }
}
