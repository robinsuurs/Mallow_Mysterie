using System;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Editor.Nodes;
using Dialogue.Runtime;
using Dialogue.RunTime;
using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace Dialogue.Editor.Graph
{
    public class StoryGraphView : GraphView {
        public readonly Vector2 DefaultNodeSize = new Vector2(2000, 1500);
        public readonly Vector2 DefaultCommentBlockSize = new Vector2(3000, 2000);
        public DialogueNode EntryPointNode;
        public Blackboard Blackboard = new Blackboard();
        public List<ExposedProperty> ExposedProperties { get; private set; } = new List<ExposedProperty>();
        private NodeSearchWindow _searchWindow;
        private readonly ItemDataNamesRetriever itemDataNames = new ItemDataNamesRetriever();
        private readonly QuestionAnswerRetriever questionAnswers = new QuestionAnswerRetriever();

        public StoryGraphView(StoryGraph editorWindow) {
            styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(GetEntryPointNodeInstance());

            AddSearchWindow(editorWindow);
        }


        private void AddSearchWindow(StoryGraph editorWindow)
        {
            _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            _searchWindow.Configure(editorWindow, this);
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }


        public void ClearBlackBoardAndExposedProperties()
        {
            ExposedProperties.Clear();
            Blackboard.Clear();
        }
        
        public Group CreateCommentBlock(Rect rect, CommentBlockData commentBlockData = null)
        {
            if(commentBlockData==null)
                commentBlockData = new CommentBlockData();
            var group = new Group
            {
                autoUpdateGeometry = true,
                title = commentBlockData.Title
            };
            AddElement(group);
            group.SetPosition(rect);
            return group;
        }

        public void AddPropertyToBlackBoard(ExposedProperty property, bool loadMode = false)
        {
            var localPropertyName = property.PropertyName;
            var localPropertyValue = property.PropertyValue;
            if (!loadMode)
            {
                while (ExposedProperties.Any(x => x.PropertyName == localPropertyName))
                    localPropertyName = $"{localPropertyName}(1)";
            }

            var item = ExposedProperty.CreateInstance();
            item.PropertyName = localPropertyName;
            item.PropertyValue = localPropertyValue;
            ExposedProperties.Add(item);

            var container = new VisualElement();
            var field = new BlackboardField {text = localPropertyName, typeText = "string"};
            container.Add(field);

            var propertyValueTextField = new TextField("Value:")
            {
                value = localPropertyValue
            };
            propertyValueTextField.RegisterValueChangedCallback(evt =>
            {
                var index = ExposedProperties.FindIndex(x => x.PropertyName == item.PropertyName);
                ExposedProperties[index].PropertyValue = evt.newValue;
            });
            var sa = new BlackboardRow(field, propertyValueTextField);
            container.Add(sa);
            Blackboard.Add(container);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            var startPortView = startPort;

            ports.ForEach((port) =>
            {
                var portView = port;
                if (startPortView != portView && startPortView.node != portView.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
        
        

        public void CreateNewDialogueNode(Vector2 position) {
            DialogueNodeData tempNode = new DialogueNodeData() { nodeGuid = Guid.NewGuid().ToString(), ItemPortCombis = new List<ItemPortCombi>()};
            AddElement(CreateNode(tempNode, position));
        }
        
        public void CreateNewDialogueEndNode(Vector2 position) {
            DialogueEndNodeData tempNode = new DialogueEndNodeData() { nodeGuid = Guid.NewGuid().ToString()};
            AddElement(CreateNode(tempNode, position));
        }
        
        public DialogueEndNode CreateNode(DialogueEndNodeData dialogueEndNodeData, Vector2 position) {
            var tempDialogueNode = new DialogueEndNode(dialogueEndNodeData);
            
            tempDialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            
            var inputPort = GetPortInstance(tempDialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "";
            tempDialogueNode.inputContainer.Add(inputPort);
            
            var dialogueTextLongField = new TextField(string.Empty)
            {
                label = "DialogueText",
                multiline = true,
            };
            dialogueTextLongField.AddToClassList("textarea");
            dialogueTextLongField.RegisterValueChangedCallback(evt => { tempDialogueNode.DialogueText = evt.newValue; });
            dialogueTextLongField.SetValueWithoutNotify(tempDialogueNode.DialogueText);
            tempDialogueNode.mainContainer.Add(dialogueTextLongField);
            
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts(); 
            tempDialogueNode.SetPosition(new Rect(position, DefaultNodeSize));
            
            return tempDialogueNode;
        }

        public DialogueNode CreateNode(DialogueNodeData dialogueNodeData, Vector2 position, bool useDefaultValues = true) {
            var tempDialogueNode = new DialogueNode(dialogueNodeData);
            
            tempDialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            
            var inputPort = GetPortInstance(tempDialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "";
            tempDialogueNode.inputContainer.Add(inputPort);
            
            var button = new Button(() => { CreateChoicePort(tempDialogueNode, "", useDefaultValues); }) {
                text = "Choice"
            };
            tempDialogueNode.titleButtonContainer.Add(button);
            
            var buttonItem = new Button(() => { CreateChoicePort(tempDialogueNode, "item", useDefaultValues); }) {
                text = "Item"
            };
            tempDialogueNode.titleButtonContainer.Add(buttonItem);
            
            var buttonSkip = new Button(() => { CreateChoicePort(tempDialogueNode, "skip", useDefaultValues); }) {
                text = "Skip"
            };
            tempDialogueNode.titleButtonContainer.Add(buttonSkip);
            
            var buttonQuestion = new Button(() => { CreateChoicePort(tempDialogueNode, "question", useDefaultValues); }) {
                text = "Question"
            };
            tempDialogueNode.titleButtonContainer.Add(buttonQuestion);
            
            int defaultIndex = useDefaultValues ? 0 : ExposedProperties.FindIndex(x => x.PropertyName == tempDialogueNode.SpeakerName);

            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;

            PopupField<string> speakerIdEnumField =
                new PopupField<string>(ExposedProperties.Select(x => x.PropertyName).ToList(), defaultIndex);
            tempDialogueNode.titleContainer.Add(speakerIdEnumField);
            speakerIdEnumField.RegisterValueChangedCallback(evt => {
                tempDialogueNode.SpeakerName = evt.newValue;
            });
            
            speakerIdEnumField.SendToBack();
            
            fillMainContainer(tempDialogueNode, useDefaultValues);
            
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts(); 
           tempDialogueNode.SetPosition(new Rect(position, DefaultNodeSize));
            
            return tempDialogueNode;
        }

        public void CreateChoicePort(DialogueNode nodeCache, string typeChoice, bool useDefaultValues, string overriddenPortName = "") {
            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            var portLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(portLabel);

            var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
            var outputPortName = string.IsNullOrEmpty(overriddenPortName)
                ? $"Option {outputPortCount + 1}"
                : overriddenPortName;
            
            var textField = new TextField()
            {
                name = String.Empty,
                value = outputPortName,
                multiline = true
            };
            textField.style.width = 130;
            textField.AddToClassList("textarea");
            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            
            var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };

            switch (typeChoice) {
                case "item" :
                    ItemPort(nodeCache, useDefaultValues, generatedPort, outputPortName);
                    break;
                case "skip" :
                    SkipPort(nodeCache, useDefaultValues, generatedPort, outputPortName, textField);
                    break;
                case "question" :
                    QuestionPort(nodeCache, useDefaultValues, generatedPort, outputPortName, textField);
                    break;
            }
            
            
            generatedPort.style.display = DisplayStyle.Flex;
            generatedPort.contentContainer.style.minHeight = 40;
            generatedPort.style.height = 40;
            
            generatedPort.contentContainer.Add(textField);
            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }

        private void ItemPort (DialogueNode nodeCache, bool useDefaultValues, Port generatedPort, string overriddenPortName) {
            int defaultIndex = useDefaultValues ? 0 : getNumberItem(nodeCache, overriddenPortName);

            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;
            
            PopupField<string> itemNeeded = new PopupField<string>(itemDataNames.itemNames.Select(x => x).ToList(), defaultIndex);
            
            itemNeeded.RegisterValueChangedCallback(evt => {
                foreach (var itemPortCombi in nodeCache.ItemPortCombis.Where(itemPortCombi => itemPortCombi.portname.Equals(overriddenPortName))) {
                    itemPortCombi.itemName = evt.newValue;
                    return;
                }
            });
            itemNeeded.style.width = 80;

            if (useDefaultValues) {
                ItemPortCombi itemPortCombiTemp = new ItemPortCombi(overriddenPortName, itemNeeded.value);
                nodeCache.ItemPortCombis.Add(itemPortCombiTemp);
            }
            
            generatedPort.contentContainer.Add(itemNeeded);
        }

        private void SkipPort(DialogueNode nodeCache, bool useDefaultValues, Port generatedPort,
            string overriddenPortName,
            TextField textField) {
            Label label = new Label("Skip option tekst");
            label.style.width = 86;
            generatedPort.Add(label);
            if (useDefaultValues) {
                nodeCache.SkipPorts.Add(overriddenPortName);
            }
            
            textField.RegisterValueChangedCallback(evt => {
                generatedPort.portName = evt.newValue;
                nodeCache.SkipPorts.Remove(evt.previousValue);
                nodeCache.SkipPorts.Add(evt.newValue);
            });
        }
        
        private void QuestionPort(DialogueNode nodeCache, bool useDefaultValues, Port generatedPort,
            string overriddenPortName, TextField textField) {
            int defaultIndex = useDefaultValues ? 0 : getNumberAnswer(nodeCache, overriddenPortName);
            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;
            
            PopupField<string> Answers = new PopupField<string>(questionAnswers.answersList.Select(x => x.answer).ToList(), defaultIndex);
            
            Answers.RegisterValueChangedCallback(evt => {
                foreach (var combi in nodeCache.QuestionAnswerPortCombis.Where(combi => combi.portname.Equals(overriddenPortName))) {
                    combi.answerUID = questionAnswers.getAnswerConnectedUID(evt.newValue);
                    return;
                }
            });
            Answers.style.width = 80;
            generatedPort.contentContainer.Add(Answers);
            
            defaultIndex = useDefaultValues ? 0 : getNumberQuestion(nodeCache, overriddenPortName);
            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;
            
            PopupField<string> Questions = new PopupField<string>(questionAnswers.questionNames.Select(x => x.question).ToList(), defaultIndex);
            
            Questions.RegisterValueChangedCallback(evt => {
                foreach (var combi in nodeCache.QuestionAnswerPortCombis.Where(combi => combi.portname.Equals(overriddenPortName))) {
                    combi.questionUID = questionAnswers.getQuesitonConnectedUID(evt.newValue);
                    return;
                }
            });
            Questions.style.width = 80;
            generatedPort.contentContainer.Add(Questions);
            
            string questionUid = null;
            foreach (var question in questionAnswers.questionNames.Where(question => question.question.Equals(Questions.value))) {
                questionUid = question.questionUID;
            }
            
            string answerUid = null;
            foreach (var answer in questionAnswers.answersList.Where(answers => answers.answer.Equals(Answers.value))) {
                answerUid = answer.answerUID;
            }

            if (useDefaultValues) {
                QuestionAnswerPortCombi questionAnswerPortCombi = new QuestionAnswerPortCombi(GUID.Generate().ToString(), questionUid, answerUid);
                textField.value = questionAnswerPortCombi.portname;
                nodeCache.QuestionAnswerPortCombis.Add(questionAnswerPortCombi);
            }
        }

        private int getNumberItem(DialogueNode dialogueNode, string overriddenPortName) {
            foreach (var name in dialogueNode.ItemPortCombis.Where(name => name.portname.Equals(overriddenPortName))) {
                for (int i = 0; i < itemDataNames.itemNames.Count; i++) {
                    if (itemDataNames.itemNames[i] == name.itemName) {
                        return i;
                    }
                }
            }
            return 0;
        }
        
        private int getNumberQuestion(DialogueNode dialogueNode, string overriddenPortName) {
            foreach (var name in dialogueNode.QuestionAnswerPortCombis.Where(name => name.portname.Equals(overriddenPortName))) {
                for (int i = 0; i < questionAnswers.questionNames.Count; i++) {
                    if (questionAnswers.questionNames[i].questionUID == name.questionUID) {
                        return i;
                    }
                }
            }
            return 0;
        }
        
        private int getNumberAnswer(DialogueNode dialogueNode, string overriddenPortName) {
            foreach (var name in dialogueNode.QuestionAnswerPortCombis.Where(name => name.portname.Equals(overriddenPortName))) {
                for (int i = 0; i < questionAnswers.answersList.Count; i++) {
                    if (questionAnswers.answersList[i].answerUID == name.answerUID) {
                        return i;
                    }
                }
            }
            return 0;
        }

        private void RemovePort(DialogueNode node, Port socket)
        {
            var targetEdge = edges.ToList()
                .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
            if (targetEdge.Any())
            {
                var edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }
            
            for (int i = node.ItemPortCombis.Count -1; i >=0; i-- ) {
                if (node.ItemPortCombis[i].portname.Equals(socket.portName)) {
                    node.ItemPortCombis.Remove(node.ItemPortCombis[i]);
                }
            }
            
            for (int i = node.SkipPorts.Count -1; i >=0; i-- ) {
                if (node.SkipPorts[i].Equals(socket.portName)) {
                    node.SkipPorts.Remove(node.SkipPorts[i]);
                }
            }

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }
        
        private void fillMainContainer(DialogueNode tempDialogueNode, bool useDefaultValues) {
            var dialogueTextLongField = new TextField(string.Empty)
            {
                label = "DialogueText",
                multiline = true,
            };
            dialogueTextLongField.AddToClassList("textarea");
            dialogueTextLongField.RegisterValueChangedCallback(evt => { tempDialogueNode.DialogueText = evt.newValue; });
            dialogueTextLongField.SetValueWithoutNotify(tempDialogueNode.DialogueText);
            tempDialogueNode.mainContainer.Add(dialogueTextLongField);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteLeft = new TextField(string.Empty)
            {
                label = "SpeakerSpriteLeft",
                multiline = true
            };
            speakerSpriteLeft.AddToClassList("textarea");
            speakerSpriteLeft.RegisterValueChangedCallback(evt => { tempDialogueNode.SpeakerSpriteLeft = evt.newValue; });
            speakerSpriteLeft.SetValueWithoutNotify(tempDialogueNode.SpeakerSpriteLeft);
            tempDialogueNode.mainContainer.Add(speakerSpriteLeft);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteRight = new TextField(string.Empty)
            {
                label = "SpeakerSpriteRight",
                multiline = true
            };
            speakerSpriteRight.AddToClassList("textarea");
            speakerSpriteRight.RegisterValueChangedCallback(evt => { tempDialogueNode.SpeakerSpriteRight = evt.newValue; });
            speakerSpriteRight.SetValueWithoutNotify(tempDialogueNode.SpeakerSpriteRight);
            tempDialogueNode.mainContainer.Add(speakerSpriteRight);
            
            int LeftorRight = useDefaultValues ? 0 : tempDialogueNode.SpeakerNameLocation.Equals("Speaker Name Left") ? 0 : 1;
            
            List<string> leftRight = new List<string>();
            leftRight.Add("Speaker Name Left");
            leftRight.Add("Speaker Name Right");
            
            //Where does the speakerName need to be
            PopupField<string> LeftRightPopUp = new PopupField<string>(leftRight.Select(x => x).ToList(), LeftorRight);
            LeftRightPopUp.RegisterValueChangedCallback(evt => {
                tempDialogueNode.SpeakerNameLocation = evt.newValue;
            });
            tempDialogueNode.mainContainer.Add(LeftRightPopUp);

            if (useDefaultValues) {
                tempDialogueNode.SpeakerNameLocation = LeftRightPopUp.value;
            }
            
            //SpeakerSprite, which sprite does there need to be shown
            var cutSceneImageTitle = new TextField(string.Empty)
            {
                label = "CutSceneImageTitle",
                multiline = true
            };
            cutSceneImageTitle.AddToClassList("textarea");
            cutSceneImageTitle.RegisterValueChangedCallback(evt => { tempDialogueNode.CutSceneImageName = evt.newValue; });
            cutSceneImageTitle.SetValueWithoutNotify(tempDialogueNode.CutSceneImageName);
            tempDialogueNode.mainContainer.Add(cutSceneImageTitle);

            //Check for if the player already had the conversation so it will become skip able 
            BaseBoolField CanSkipFromThisPoint = new Toggle() {
                label = "Already had conversation"
            };
            CanSkipFromThisPoint.SetValueWithoutNotify(tempDialogueNode.CanSkipFromThisPoint);
            tempDialogueNode.mainContainer.Add(CanSkipFromThisPoint);
            CanSkipFromThisPoint.RegisterValueChangedCallback(evt => {
                tempDialogueNode.CanSkipFromThisPoint = evt.newValue;
            });
        }

        private Port GetPortInstance(DialogueNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }
        
        private Port GetPortInstance(DialogueEndNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

        private DialogueNode GetEntryPointNodeInstance()
        {
            var nodeCache = new DialogueNode()
            {
                title = "START",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "ENTRYPOINT",
                EntyPoint = true
            };

            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            generatedPort.portName = "Next";
            nodeCache.outputContainer.Add(generatedPort);

            nodeCache.capabilities &= ~Capabilities.Movable;
            nodeCache.capabilities &= ~Capabilities.Deletable;

            nodeCache.RefreshExpandedState();
            nodeCache.RefreshPorts();
            nodeCache.SetPosition(new Rect(100, 200, 100, 150));
            return nodeCache;
        }
    }
}