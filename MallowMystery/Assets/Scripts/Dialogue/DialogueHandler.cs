
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Runtime;
using Dialogue.RunTime;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI DialogueBoxUI;
    [SerializeField] private TextMeshProUGUI SpeakerNameBoxLeft;
    [SerializeField] private GameObject SpeakerNameLeftNameBox;
    [SerializeField] private Image SpeakerSpriteLeft;
    [SerializeField] private TextMeshProUGUI SpeakerNameBoxRight;
    [SerializeField] private GameObject SpeakerNameRightNameBox;
    [SerializeField] private Image SpeakerSpriteRight;
    [SerializeField] private Button ChoicesButton;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private float textspeed;
    [SerializeField] private ListOfSprites _listOfSprites;
    [SerializeField] private InputActionAsset _inputAction;

    private string overwriteGUID;
    private bool overwrite;
    private List<Question> listOfQuestions;
    
    private DialogueContainer dialogue;
    private IEnumerable<NodeLinkData> choices = new List<NodeLinkData>();
    public string currentDialogue;
    private bool singleOption;
    private bool inDialogue;
    
    private void Start() {
        listOfQuestions = Resources.LoadAll<Question>("QuestionAnswers/Questions").ToList();
    }

    public void StartDialogue(DialogueContainer dialogueContainer) {
        if (!inDialogue) {
            dialogue = dialogueContainer;
            dialogueCanvas.SetActive(true);
            var narrativeData = dialogueContainer.NodeLinks.Where(x => x.PortName.Equals("Next")).ToList()[0].TargetNodeGUID;
            ProceedToNarrative(narrativeData);
            inDialogue = true;
            disableInputActions();
        }
    }

    private void EndDialogue() {
        dialogue.alreadyHadConversation = true;
        currentDialogue = null;
        singleOption = false;
        inDialogue = false;
        DialogueBoxUI.text = "";
        SpeakerNameBoxLeft.text = "";
        SpeakerNameBoxRight.text = "";
        dialogueCanvas.SetActive(false);
        enableInputActions();
    }
    
    void Update()
    {
        if (inDialogue && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))) {
            if (DialogueBoxUI.text == currentDialogue) {
                if (!choices.Any()) {
                    EndDialogue();
                } else if (overwrite) {
                    overwrite = false;
                    ProceedToNarrative(overwriteGUID);
                } else if (singleOption) {
                    ProceedToNarrative(choices.First().TargetNodeGUID);
                }
            } else {
                StopAllCoroutines();
                DialogueBoxUI.text = currentDialogue;
            }
        }
    }
    
    private void ProceedToNarrative(string narrativeDataGUID) {
        var currentNode = dialogue.DialogueNodeData.FirstOrDefault(x => x.nodeGuid == narrativeDataGUID);
        if (currentNode == null) {
            EndingNode(narrativeDataGUID);
        } else if (currentNode.dialogueText.Equals("") && currentNode.SpeakerSpriteLeft.Equals("") && currentNode.SpeakerSpriteRight.Equals("")) { //TODO BM: 15-10-2023 change this
            EndDialogue();
        } else if (currentNode.canSkipFromThisPoint && !dialogue.alreadyHadConversation) {
            choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID).ToList();
            ProceedToNarrative(choices.First().TargetNodeGUID);
        } else {
            choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID).ToList();
            currentDialogue = ProcessProperties(currentNode.dialogueText);
            DialogueBoxUI.text = "";
            StartCoroutine(TypeLine());
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
        
            foreach (var t in buttons) {
                Destroy(t.gameObject);
            }
            
            buttonContainer.gameObject.SetActive(false);
        
            _listOfSprites.CharacterSetter(currentNode.SpeakerSpriteLeft, currentNode.SpeakerSpriteRight);
            
            setSpeakers(currentNode);
            
            _listOfSprites.CutSceneImageSetter(currentNode.CutSceneImageName);
            

            if (choices.Any(choice => currentNode.QuestionAnswerPortCombis.Any(x => x.portname.Equals(choice.PortName)))) {
                overwrite = true;
                
                foreach (var questionAnswerPort in listOfQuestions.SelectMany(question => currentNode.QuestionAnswerPortCombis.Where(questionAnswerPort => question.UID.Equals(questionAnswerPort.questionUID) && question.getChosenAnswer().UID.Equals(questionAnswerPort.answerUID)))) {
                    foreach (var choice in choices) {
                        if (!choice.PortName.Equals(questionAnswerPort.portname)) continue;
                        
                        singleOption = true;
                        overwriteGUID = choice.TargetNodeGUID;
                        return;
                    }
                }
                
            } else if (choices.Count() is 1 or 0 || (choices.Count() >= 2 && CanSkip(currentNode, choices))) {
                singleOption = true;
            } else {
                ShowButtons();
            }   
        }
    }

    private void ShowButtons() {
        singleOption = false;
        buttonContainer.gameObject.SetActive(true);
        foreach (var choice in choices) {
            Button button = Instantiate(ChoicesButton, buttonContainer);
            button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
            button.GetComponentInChildren<Text>().fontSize = 24;
            button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
        }
    }

    private void EndingNode(string narrativeDataGuid) {
        EndDialogue();
        dialogue.DialogueEndNodeData.FirstOrDefault(x => x.nodeGuid == narrativeDataGuid)!.DialogueEvent.Invoke();
    }

    private void setSpeakers(DialogueNodeData currentNode) {
        switch (currentNode.SpeakerNameLocation) {
            case "Speaker Name Left":
                SpeakerNameBoxLeft.text = currentNode.SpeakerName;
                SpeakerNameLeftNameBox.SetActive(true);
                SpeakerNameRightNameBox.SetActive(false);
                break;
            case "Speaker Name Right":
                SpeakerNameBoxRight.text = currentNode.SpeakerName;
                SpeakerNameLeftNameBox.SetActive(false);
                SpeakerNameRightNameBox.SetActive(true);
                break;
            case "None":
                SpeakerNameRightNameBox.SetActive(false);
                SpeakerNameLeftNameBox.SetActive(false);
                break;
        }
    }

    private bool CanSkip(DialogueNodeData dialogueNodeData, IEnumerable<NodeLinkData> nodeLinkDatas) {
        if (dialogueNodeData.SkipPorts.Count() != 0) {
            bool test = nodeLinkDatas.Any(choice => dialogueNodeData.SkipPorts.Any(x => x.Equals(choice.PortName)));
            return !(dialogue.alreadyHadConversation && test);
        } else {
            return false;
        }
        
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

    private void enableInputActions() {
        _inputAction.Enable();
    }
    
    private void disableInputActions() {
        _inputAction.Disable();
    }
}
