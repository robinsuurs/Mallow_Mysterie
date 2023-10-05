using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue.Runtime
{
    [Serializable]
    public class DialogueNodeData {
        public string nodeGuid;
        public string dialogueText;
        public Vector2 position;
        [FormerlySerializedAs("SpeakerId")] public string SpeakerName;
        public string SpeakerSpriteLeft;
        public string SpeakerSpriteRight;
        public string ItemId;
    }
}