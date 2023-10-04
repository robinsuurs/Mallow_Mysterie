using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Runtime;
using ScriptObjects;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DialogueBoxUI;
    [SerializeField] private TextMeshProUGUI SpeakerNameBox;
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
        dialogue = dialogueContainer;
        DialogueCanvas.SetActive(true);
        var narrativeData = dialogue.NodeLinks.First();
        ProceedToNarrative(narrativeData.TargetNodeGUID);
        inDialogue = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inDialogue) {
            if (DialogueBoxUI.text == currentDialogue) {
                if (!choices.Any()) {
                    currentDialogue = null;
                    singleOption = false;
                    inDialogue = false;
                    DialogueBoxUI.text = "";
                    SpeakerNameBox.text = "";
                    DialogueCanvas.SetActive(false);
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
        
        SpeakerNameBox.text = currentNode.SpeakerName;

        if (choices.Count() == 1) {
            singleOption = true;
        } else {
            // TODO: BM 04-10-2023 What to do with multiple buttons but only one can be shown based on conditions
            singleOption = false;
            foreach (var choice in choices) {
                
                var button = Instantiate(ChoicesButton, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
                
                if (_inventory != null && dialogue.DialogueNodeData.Find(x => x.nodeGuid == choice.TargetNodeGUID).ItemId != "") {
                    button.interactable = ItemNeededInInventory(choice.TargetNodeGUID);
                }
            }
        }
    }

    //TODO: BM 04-10-2023 what inventory do we grab? -> link to player?
    //TODO: BM 04-10-2023 add boolean on pickup condition, now it always has a null value
    private bool ItemNeededInInventory(string choiceTargetNodeGuid) {
        var optionNode = dialogue.DialogueNodeData.Find(x => x.nodeGuid == choiceTargetNodeGuid).ItemId;
        return _inventory.items.Any(item => optionNode == item.itemName);
        // && item.hasBeenPickedUp
    }
    
    private string ProcessProperties(string text) {
        return dialogue.ExposedProperties.Aggregate(text, (current, exposedProperty) => current.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue));
    }

    IEnumerator TypeLine() {
        foreach (var c in currentDialogue.ToCharArray()) {
            DialogueBoxUI.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
}
