using System;
using System.Collections.Generic;
using Dialogue.Runtime;
using Subtegral.DialogueSystem.DataContainers;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue.RunTime
{
    [Serializable]
    public class DialogueContainer : ScriptableObject
    {
        public bool alreadyHadConversation = false;
        public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
        public List <DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
        public List<DialogueEndNodeData> DialogueEndNodeData = new List<DialogueEndNodeData>();
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        public List<CommentBlockData> CommentBlockData = new List<CommentBlockData>();
    }
}