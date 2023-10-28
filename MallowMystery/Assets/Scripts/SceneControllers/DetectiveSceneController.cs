using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

public class DetectiveSceneController : MonoBehaviour {
    [SerializeField] private GameObject detectiveCharacter;
    private void Start() {
        var progression = DataPersistenceManager.instance.getProgession();
        if (progression == ProgressionEnum.gameProgression.talkToDetectiveInOffice) {
            detectiveCharacter.GetComponent<DialogueSender>().setDialogueContainer
                (Resources.LoadAll("Dialogues/Scene1 Dialogue", typeof(DialogueContainer)).Cast<DialogueContainer>().First());
        }
    }
}
