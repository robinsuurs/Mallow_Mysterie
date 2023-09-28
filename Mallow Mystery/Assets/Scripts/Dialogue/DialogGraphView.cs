using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(200, 150);
    
    public DialogGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0,grid);
        grid.StretchToParentSize();
        
        AddElement(GenerateEntryPointNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compactiblePorts = new List<Port>();
        
        ports.ForEach(port =>
        {
            if (startPort!=port && startPort.node!=port.node)
            {
                compactiblePorts.Add(port);
            }
        });
        
        return compactiblePorts;
    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            DialogueText = "Banana",
            EntryPoint = true
        };

        var generatePort = GeneratePort(node, Direction.Output);
        generatePort.portName = "next";
        node.outputContainer.Add(generatePort);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        
        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogueNode()
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        var button = new Button(() => { AddChoicePort(dialogueNode); });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);
        
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        return dialogueNode;
    }

    // public void AddChoicePort(DialogueNode dialogueNode, string overriddenPortName = "")
    // {
    //     var generatedPort = GeneratePort(dialogueNode, Direction.Output);
    //
    //     var oldLabel = generatedPort.contentContainer.Q<Label>("type");
    //     generatedPort.contentContainer.Remove(oldLabel);
    //
    //     var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
    //     generatedPort.portName = $"Choice {outputPortCount}";
    //
    //     var choicePortName = string.IsNullOrEmpty(overriddenPortName)
    //         ? $"Choice {outputPortCount + 1}"
    //         : overriddenPortName;
    //
    //     var textField = new TextField
    //     {
    //         name = string.Empty,
    //         value = choicePortName
    //     };
    //     textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
    //     
    //     generatedPort.contentContainer.Add(new Label(""));
    //     generatedPort.contentContainer.Add(textField);
    //     var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort))
    //     {
    //         text = "X"
    //     };
    //     generatedPort.contentContainer.Add(deleteButton);
    //     
    //     generatedPort.portName = choicePortName;
    //     dialogueNode.outputContainer.Add(generatedPort);
    //     dialogueNode.RefreshPorts();
    //     dialogueNode.RefreshExpandedState();
    // }
    
    public void AddChoicePort(DialogueNode nodeCache, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(nodeCache, Direction.Output);
        var portLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(portLabel);

        var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
        var outputPortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Option {outputPortCount + 1}"
            : overriddenPortName;


        var textField = new TextField()
        {
            name = string.Empty,
            value = outputPortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
        {
            text = "X"
        };
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
}
