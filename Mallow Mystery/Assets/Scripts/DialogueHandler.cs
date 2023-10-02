using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] private Button ChoicesButton;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private float textspeed;

    [SerializeField] private GameEventStandardAdd showDialogueUi;
    [SerializeField] private GameEventStandardAdd hideDialogueUi;

    private IEnumerable<NodeLinkData> choices = new List<NodeLinkData>();
    public string currentDialogue;
    private bool singleOption = false;
    private bool inDialogue = false;
    
    public void StartDialogue()
    {
        showDialogueUi.Raise();
        var narrativeData = dialogue.NodeLinks.First();
        ProceedToNarrative(narrativeData.TargetNodeGUID);
        inDialogue = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inDialogue)
        {
            if (textMeshProUGUI.text == currentDialogue)
            {
                if (!choices.Any())
                {
                    currentDialogue = null;
                    singleOption = false;
                    textMeshProUGUI.text = "";
                    inDialogue = false;
                    hideDialogueUi.Raise();
                }
                else if (singleOption)
                {
                    ProceedToNarrative(choices.First().TargetNodeGUID);
                }
            }
            else
            {
                StopAllCoroutines();
                textMeshProUGUI.text = currentDialogue;
            }
        }
    }
    
    private void ProceedToNarrative(string narrativeDataGUID)
    {
        var text = dialogue.DialogueNodeData.Find(x => x.nodeGuid == narrativeDataGUID).dialogueText;
        choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
        currentDialogue = ProcessProperties(text);
        textMeshProUGUI.text = "";
        StartCoroutine(TypeLine());
        var buttons = buttonContainer.GetComponentsInChildren<Button>();
        
        foreach (var t in buttons)
        {
            Destroy(t.gameObject);
        }

        if (choices.Count() == 1)
        {
            singleOption = true;
        }
        else
        {
            singleOption = false;
            foreach (var choice in choices)
            {
                var button = Instantiate(ChoicesButton, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
            }
        }
    }
    
    private string ProcessProperties(string text)
    {
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }
        return text;
    }

    // public void startDialogue()
    // {
    //     if (courotineRunning) return;
    //     courotineRunning = true;
    //     index = 0;
    //     textMeshProUGUI.text = String.Empty;
    //     StartCoroutine(TypeLine());
    // }

    IEnumerator TypeLine()
    {
        foreach (var c in currentDialogue.ToCharArray())
        {
            textMeshProUGUI.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }

    // void NextLine()
    // {
    //     if (index < text.Length - 1)
    //     {
    //         index++;
    //         textMeshProUGUI.text = String.Empty;
    //         StartCoroutine(TypeLine());
    //     }
    //     else
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }
}
