using System;
using System.Collections.Generic;
using Dialogue.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable]
    public class DialogueContainer : ScriptableObject
    {
        public bool alreadyHadConversation = false;
        public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
        public List <DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        public List<CommentBlockData> CommentBlockData = new List<CommentBlockData>();
    }
}