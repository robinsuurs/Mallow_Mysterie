using Dialogue.Runtime;
using UnityEditor.Experimental.GraphView;

namespace Dialogue.Editor.Nodes
{
    public class DialogueNode : Node {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
        public string SpeakerName;
        public string SpeakerSpriteLeft;
        public string SpeakerSpriteRight;
        public string ItemId;
        
        public DialogueNode (DialogueNodeData data) {
            DialogueText = data.dialogueText;
            GUID = data.nodeGuid;
            SpeakerName = data.SpeakerId;
            SpeakerSpriteLeft = data.SpeakerSpriteLeft;
            SpeakerSpriteRight = data.SpeakerSpriteRight;
            ItemId = data.ItemId;
        }
        
        public DialogueNode () {
    
        }
    }
}