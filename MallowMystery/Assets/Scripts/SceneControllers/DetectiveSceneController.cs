using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.RunTime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;

public class DetectiveSceneController : MonoBehaviour {
    [SerializeField] private GameObject detectiveCharacter;
    private void Start() {
        var progression = DataPersistenceManager.instance.getProgession();
        switch (progression) {
            case ProgressionEnum.gameProgression.talkToDetectiveInOffice:
                detectiveCharacter.GetComponent<DialogueSender>().setDialogueContainer
                    (Resources.LoadAll("Dialogues/test1", typeof(DialogueContainer)).Cast<DialogueContainer>().First());
                break;
            case ProgressionEnum.gameProgression.toFriendsHouse:
                detectiveCharacter.SetActive(false);
                break;
        }
    }
}
