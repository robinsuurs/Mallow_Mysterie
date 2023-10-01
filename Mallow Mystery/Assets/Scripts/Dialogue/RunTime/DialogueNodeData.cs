using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Subtegral.DialogueSystem.DataContainers
{
    [Serializable]
    public class DialogueNodeData
    {
        public string nodeGuid;
        public string dialogueText;
        public Vector2 position;
    }
}