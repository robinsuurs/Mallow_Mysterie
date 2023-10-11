using System;
using System.Collections.Generic;
using System.Linq;
using Dialogue.Editor.Nodes;
using Dialogue.Runtime;
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
        private ItemDataNamesRetriever itemDataNames = new ItemDataNamesRetriever();

        public StoryGraphView(StoryGraph editorWindow) {
            styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
            // styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
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
            DialogueNodeData tempNode = new DialogueNodeData() { nodeGuid = Guid.NewGuid().ToString() };
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
            
            var buttonItem = new Button(() => { AddChoiceItemPort(tempDialogueNode); }) {
                text = "Add Choice With Item"
            };
            tempDialogueNode.titleButtonContainer.Add(buttonItem);
            
            // TODO: Bram Mulders 01-10-2023, fix save for this
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
                style = { width = 300 },
                multiline = true
            };
            dialogueTextLongField.RegisterValueChangedCallback((evt => { tempDialogueNode.DialogueText = evt.newValue; }));
            dialogueTextLongField.SetValueWithoutNotify(tempDialogueNode.DialogueText);
            tempDialogueNode.mainContainer.Add(dialogueTextLongField);
            
            //ItemId textbox
            var inventoryItemId = new TextField(string.Empty)
            {
                label = "ItemId",
                style = { width = 300 },
                multiline = true
            };
            inventoryItemId.RegisterValueChangedCallback((evt => { tempDialogueNode.ItemId = evt.newValue; }));
            inventoryItemId.SetValueWithoutNotify(tempDialogueNode.ItemId);
            tempDialogueNode.mainContainer.Add(inventoryItemId);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteLeft = new TextField(string.Empty)
            {
                label = "SpeakerSpriteLeft",
                style = { width = 300 },
                multiline = true
            };
            speakerSpriteLeft.RegisterValueChangedCallback((evt => { tempDialogueNode.SpeakerSpriteLeft = evt.newValue; }));
            speakerSpriteLeft.SetValueWithoutNotify(tempDialogueNode.SpeakerSpriteLeft);
            tempDialogueNode.mainContainer.Add(speakerSpriteLeft);
            
            //SpeakerSprite, which sprite does there need to be shown
            var speakerSpriteRight = new TextField(string.Empty)
            {
                label = "SpeakerSpriteRight",
                style = { width = 300 },
                multiline = true
            };
            speakerSpriteRight.RegisterValueChangedCallback((evt => { tempDialogueNode.SpeakerSpriteRight = evt.newValue; }));
            speakerSpriteRight.SetValueWithoutNotify(tempDialogueNode.SpeakerSpriteRight);
            tempDialogueNode.mainContainer.Add(speakerSpriteRight);
            
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts();
            tempDialogueNode.SetPosition(new Rect(position, DefaultNodeSize));
            
            return tempDialogueNode;
        }


        public void AddChoicePort(DialogueNode nodeCache, string overriddenPortName = "", string OverridenItemIdRequired = "")
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
                value = outputPortName
            };
            
            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            textField.StretchToParentWidth();
            
            var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };
            textField.style.width = 130;
            textField.multiline = true;
            textField.style.position = Position.Relative;
            
            generatedPort.contentContainer.style.display = DisplayStyle.Flex;
            generatedPort.contentContainer.style.position = Position.Relative;
            generatedPort.contentContainer.style.alignItems = Align.FlexStart;
            
            generatedPort.contentContainer.Add(textField);
            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }
        
        public void AddChoiceItemPort(DialogueNode nodeCache, string overriddenPortName = "", string OverridenItemIdRequired = "")
        {
            var generatedPort = GetPortInstance(nodeCache, Direction.Output);
            var portLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(portLabel);

            var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
            var outputPortName = string.IsNullOrEmpty(overriddenPortName)
                ? $"Option {outputPortCount + 1}"
                : overriddenPortName;
            
            PopupField<string> itemNeeded =
                new PopupField<string>(ExposedProperties.Select(x => x.PropertyName).ToList(), 0);
            itemNeeded.RegisterValueChangedCallback(evt => {
                nodeCache.ItemId = evt.newValue;
            });
            
            var textField = new TextField()
            {
                name = string.Empty,
                value = outputPortName
            };
            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            textField.StretchToParentWidth();
            textField.style.width = 130;
            textField.multiline = true;
            textField.style.position = Position.Relative;

            
            var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };
            
            generatedPort.contentContainer.style.display = DisplayStyle.Flex;
            generatedPort.contentContainer.style.position = Position.Relative;
            generatedPort.contentContainer.style.alignItems = Align.FlexStart;
            
            generatedPort.contentContainer.Add(textField);
            generatedPort.contentContainer.Add(itemNeeded);
            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }

        private void RemovePort(Node node, Port socket)
        {
            var targetEdge = edges.ToList()
                .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
            if (targetEdge.Any())
            {
                var edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }

        private Port GetPortInstance(DialogueNode node, Direction nodeDirection,
            Port.Capacity capacity = Port.Capacity.Single)
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