using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

namespace Subtegral.DialogueSystem.Runtime
{
    public class DialogueParser : MonoBehaviour
    {
        [SerializeField] private DialogueContainer dialogue;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button ChoicesButton;
        [SerializeField] private Transform buttonContainer;
        private bool oneChoiceDialogue = false;
        private IEnumerable<NodeLinkData> choices;

        // private void Start()
        // {
        //     var narrativeData = dialogue.NodeLinks.First();
        //     ProceedToNarrative(narrativeData.TargetNodeGUID);
        // }

        public void startDialogue()
        {
            var narrativeData = dialogue.NodeLinks.First();
            ProceedToNarrative(narrativeData.TargetNodeGUID);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && oneChoiceDialogue)
            {
                oneChoiceDialogue = false;
                Debug.Log("TESSST");
                ProceedToNarrative(choices.First().TargetNodeGUID);
            }
        }

        private void ProceedToNarrative(string narrativeDataGUID)
        {
            oneChoiceDialogue = false;
            var text = dialogue.DialogueNodeData.Find(x => x.nodeGuid == narrativeDataGUID).dialogueText;
            choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            dialogueText.text = ProcessProperties(text);
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            if (choices.Count() == 1)
            {
                oneChoiceDialogue = true;
            }
            else
            {
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
    }
}