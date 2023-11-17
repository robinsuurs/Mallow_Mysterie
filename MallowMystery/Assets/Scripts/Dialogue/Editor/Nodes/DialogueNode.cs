using System;
using System.Collections.Generic;
using Dialogue.Editor.Graph;
using Dialogue.Runtime;
using Dialogue.RunTime;
using Subtegral.DialogueSystem.Editor;
using UnityEditor.Experimental.GraphView;

namespace Dialogue.Editor.Nodes
{
    public class DialogueNode : Node {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
        public string SpeakerName;
        public string SpeakerNameLocation;
        public string SpeakerSpriteLeft;
        public string SpeakerSpriteRight;
        public List<ItemPortCombi> ItemPortCombis;
        public List<string> SkipPorts;
        public bool CanSkipFromThisPoint = false;
        public string CutSceneImageName;
        
        public DialogueNode (DialogueNodeData data) {
            DialogueText = data.dialogueText;
            GUID = data.nodeGuid;
            SpeakerName = data.SpeakerName;
            SpeakerNameLocation = data.SpeakerNameLocation;
            SpeakerSpriteLeft = data.SpeakerSpriteLeft;
            SpeakerSpriteRight = data.SpeakerSpriteRight;
            ItemPortCombis = data.ItemPortCombis;
            SkipPorts = data.SkipPorts;
            CanSkipFromThisPoint = data.canSkipFromThisPoint;
            CutSceneImageName = data.CutSceneImageName;
        }
        
        public DialogueNode () {
    
        }
    }
}