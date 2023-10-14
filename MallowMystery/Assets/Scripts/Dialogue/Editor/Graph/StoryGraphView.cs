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
        
        

        public void CreateNewDialogueNode(string nodeName, Vector2 position) {
            DialogueNodeData tempNode = new DialogueNodeData() { nodeGuid = Guid.NewGuid().ToString(), ItemPortCombis = new List<ItemPortCombi>()};
            AddElement(CreateNode(tempNode, position));
        }

        public DialogueNode CreateNode(DialogueNodeData dialogueNodeData, Vector2 position, bool useDefaultValues = true) {
            var tempDialogueNode = new DialogueNode(dialogueNodeData);
            
            tempDialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            
            var inputPort = GetPortInstance(tempDialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "";
            tempDialogueNode.inputContainer.Add(inputPort);
            
            var button = new Button(() => { AddChoicePort(tempDialogueNode); }) {
                text = "Add Choice"
            };
            tempDialogueNode.titleButtonContainer.Add(button);
            
            var buttonItem = new Button(() => { AddChoiceItemPort(tempDialogueNode, useDefaultValues = true); }) {
                text = "Add Choice With Item"
            };
            tempDialogueNode.titleButtonContainer.Add(buttonItem);
            
            int defaultIndex = useDefaultValues ? 0 : ExposedProperties.FindIndex(x => x.PropertyName == tempDialogueNode.SpeakerName);

            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;

            PopupField<string> speakerIdEnumField =
                new PopupField<string>(ExposedProperties.Select(x => x.PropertyName).ToList(), defaultIndex);
            tempDialogueNode.titleContainer.Add(speakerIdEnumField);
            speakerIdEnumField.RegisterValueChangedCallback(evt => {
                tempDialogueNode.SpeakerName = evt.newValue;
            });
            
            speakerIdEnumField.SendToBack();
            
            var dialogueTextLongField = new TextField(string.Empty)
            {
                label = "DialogueText",
                multiline = true,
            };
            dialogueTextLongField.AddToClassList("textarea");
            dialogueTextLongField.RegisterValueChangedCallback((evt => { tempDialogueNode.DialogueText = evt.newValue; }));
            dialogueTextLongField.SetValueWithoutNotify(tempDialogueNode.DialogueText);
            tempDialogueNode.mainContainer.Add(dialogueTextLongField);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteLeft = new TextField(string.Empty)
            {
                label = "SpeakerSpriteLeft",
                multiline = true
            };
            speakerSpriteLeft.AddToClassList("textarea");
            speakerSpriteLeft.RegisterValueChangedCallback((evt => { tempDialogueNode.SpeakerSpriteLeft = evt.newValue; }));
            speakerSpriteLeft.SetValueWithoutNotify(tempDialogueNode.SpeakerSpriteLeft);
            tempDialogueNode.mainContainer.Add(speakerSpriteLeft);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteRight = new TextField(string.Empty)
            {
                label = "SpeakerSpriteRight",
                multiline = true
            };
            speakerSpriteRight.AddToClassList("textarea");
            speakerSpriteRight.RegisterValueChangedCallback((evt => { tempDialogueNode.SpeakerSpriteRight = evt.newValue; }));
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

            //Check for if the player already had the conversation so it will become skip able 
            BaseBoolField alreadyHadConversation = new Toggle() {
                label = "Already had conversation"
            };
            alreadyHadConversation.SetValueWithoutNotify(tempDialogueNode.alreadyHadConversation);
            tempDialogueNode.mainContainer.Add(alreadyHadConversation);
            alreadyHadConversation.RegisterValueChangedCallback(evt => {
                tempDialogueNode.alreadyHadConversation = evt.newValue;
            });
            
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts(); 
           tempDialogueNode.SetPosition(new Rect(position, DefaultNodeSize));
            
            return tempDialogueNode;
        }


        public void AddChoicePort(DialogueNode nodeCache, string overriddenPortName = "")
        {
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
        
        public void AddChoiceItemPort(DialogueNode nodeCache, bool useDefaultValues, string overriddenPortName = "")
        {
            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            var portLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(portLabel);

            var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
            var outputPortName = string.IsNullOrEmpty(overriddenPortName)
                ? $"Option {outputPortCount + 1}"
                : overriddenPortName;

            int defaultIndex = useDefaultValues ? 0 : getNumber(nodeCache, overriddenPortName);

            defaultIndex = defaultIndex < 0 ? 0 : defaultIndex;
            
            var textField = new TextField()
            {
                name = string.Empty,
                value = outputPortName,
                multiline = true
            };
            textField.style.width = 130;
            textField.AddToClassList("textarea");
            textField.RegisterValueChangedCallback(evt => {
                foreach (var itemPortCombi in nodeCache.ItemPortCombis.Where(itemPortCombi => itemPortCombi.portname.Equals(generatedPort.portName))) {
                    itemPortCombi.portname = evt.newValue;
                    generatedPort.portName = evt.newValue;
                    return;
                }
            });
            
            PopupField<string> itemNeeded = new PopupField<string>(itemDataNames.itemNames.Select(x => x).ToList(), defaultIndex);
            
            itemNeeded.RegisterValueChangedCallback(evt => {
                foreach (var itemPortCombi in nodeCache.ItemPortCombis.Where(itemPortCombi => itemPortCombi.portname.Equals(generatedPort.portName))) {
                    itemPortCombi.itemName = evt.newValue;
                    return;
                }
            });
            itemNeeded.style.width = 80;
            
            if (useDefaultValues) {
                ItemPortCombi itemPortCombiTemp = new ItemPortCombi(outputPortName, itemNeeded.value);
                nodeCache.ItemPortCombis.Add(itemPortCombiTemp);
            }
            
            var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };
            
            generatedPort.style.display = DisplayStyle.Flex;
            generatedPort.contentContainer.style.minHeight = 40;
            generatedPort.style.height = 40;
            
            generatedPort.contentContainer.Add(textField);
            generatedPort.contentContainer.Add(itemNeeded);
            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }

        private int getNumber(DialogueNode dialogueNode, string overriddenPortName) {
            foreach (var name in dialogueNode.ItemPortCombis.Where(name => name.portname.Equals(overriddenPortName))) {
                for (int i = 0; i < itemDataNames.itemNames.Count; i++) {
                    if (itemDataNames.itemNames[i] == name.itemName) {
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

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }

        private Port GetPortInstance(DialogueNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
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