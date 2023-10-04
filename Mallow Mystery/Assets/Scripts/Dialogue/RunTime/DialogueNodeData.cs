using System;
using UnityEngine;

namespace Dialogue.Runtime
{
    [Serializable]
    public class DialogueNodeData {
        public string nodeGuid;
        public string dialogueText;
        public Vector2 position;
        public string SpeakerId;
        public string SpeakerSprite;
        public string ItemId;
    }
}