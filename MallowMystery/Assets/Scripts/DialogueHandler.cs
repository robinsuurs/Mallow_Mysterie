using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Runtime;
using Dialogue.RunTime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DialogueBoxUI;
    [SerializeField] private TextMeshProUGUI SpeakerNameBoxLeft;
    [SerializeField] private TextMeshProUGUI SpeakerNameBoxRight;
    private DialogueContainer dialogue;
    [SerializeField] private Button ChoicesButton;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private float textspeed;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject DialogueCanvas;
    [SerializeField] private ListOfSprites _listOfSprites;

    private IEnumerable<NodeLinkData> choices = new List<NodeLinkData>();
    public string currentDialogue;
    private bool singleOption;
    private bool inDialogue;
    
    public void StartDialogue(DialogueContainer dialogueContainer) {
        if (!inDialogue) {
            dialogue = dialogueContainer;
            DialogueCanvas.SetActive(true);
            var narrativeData = dialogue.NodeLinks.First();
            ProceedToNarrative(narrativeData.TargetNodeGUID);
            inDialogue = true;
            Time.timeScale = 0f;
        }
    }
    
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && inDialogue) {
            if (DialogueBoxUI.text == currentDialogue) {
                if (!choices.Any()) {
                    currentDialogue = null;
                    singleOption = false;
                    inDialogue = false;
                    DialogueBoxUI.text = "";
                    SpeakerNameBoxLeft.text = "";
                    SpeakerNameBoxRight.text = "";
                    DialogueCanvas.SetActive(false);
                    Time.timeScale = 1f;
                }
                else if (singleOption)
                {
                    ProceedToNarrative(choices.First().TargetNodeGUID);
                }
            } else {
                StopAllCoroutines();
                DialogueBoxUI.text = currentDialogue;
            }
        }
    }
    
    private void ProceedToNarrative(string narrativeDataGUID) {
        var currentNode = dialogue.DialogueNodeData.Find(x => x.nodeGuid == narrativeDataGUID);
        choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
        currentDialogue = ProcessProperties(currentNode.dialogueText);
        DialogueBoxUI.text = "";
        StartCoroutine(TypeLine());
        var buttons = buttonContainer.GetComponentsInChildren<Button>();
        
        foreach (var t in buttons) {
            Destroy(t.gameObject);
        }
        
        _listOfSprites.CharacterSetter(currentNode.SpeakerSpriteLeft, currentNode.SpeakerSpriteRight);

        if (currentNode.SpeakerNameLocation.Equals("Speaker Name Left")) {
            SpeakerNameBoxLeft.text = currentNode.SpeakerName;
            SpeakerNameBoxRight.text = "";
        } else {
            SpeakerNameBoxRight.text = currentNode.SpeakerName;
            SpeakerNameBoxLeft.text = "";
        }
    
        if (choices.Count() == 1 || choices.Count() == 0) {
            singleOption = true;
            buttonContainer.gameObject.SetActive(false);
        } else {
            // TODO: BM 04-10-2023 What to do with multiple buttons but only one can be shown based on conditions
            // TODO: BM 08-10-2023 Select Buttons without mouse?
            singleOption = false;
            buttonContainer.gameObject.SetActive(true);
            foreach (var choice in choices) {
                
                var button = Instantiate(ChoicesButton, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
                
                if (_inventory != null) {
                    button.interactable = ItemNeededInInventory(currentNode, choice.PortName);
                }
            }
        }
    }
    
    private bool ItemNeededInInventory(DialogueNodeData dialogueNodeData, string portName) {
        var name = "";
        foreach (var itemPortCombi in dialogueNodeData.ItemPortCombis.Where(itemPortCombi => itemPortCombi.portname.Equals(portName))) {
            name = itemPortCombi.itemName;
        }
        return _inventory.items.Any(item => name == item.itemName && item.hasBeenPickedUp);
    }
    
    private string ProcessProperties(string text) {
        return dialogue.ExposedProperties.Aggregate(text, (current, exposedProperty) => current.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue));
    }

    IEnumerator TypeLine() {
        foreach (var c in currentDialogue.ToCharArray()) {
            DialogueBoxUI.text += c;
            yield return new WaitForSecondsRealtime(textspeed);
        }
    }
}
