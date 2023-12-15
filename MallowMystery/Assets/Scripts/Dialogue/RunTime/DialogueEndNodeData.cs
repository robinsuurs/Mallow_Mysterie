using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue.RunTime {
    [Serializable]
    public class DialogueEndNodeData {
        public string nodeGuid;
        public string dialogueText;
        public Vector2 position;
        public UnityEvent DialogueEvent;
    }
}
