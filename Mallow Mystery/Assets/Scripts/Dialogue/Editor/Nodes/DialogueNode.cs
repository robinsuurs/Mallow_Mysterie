using Dialogue.Runtime;
using UnityEditor.Experimental.GraphView;

namespace Dialogue.Editor.Nodes
{
    public class DialogueNode : Node {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
        public string SpeakerId;
        public string SpeakerSprite;
        public string ItemId;
        
        public DialogueNode (DialogueNodeData data) {
            DialogueText = data.dialogueText;
            GUID = data.nodeGuid;
            SpeakerId = data.SpeakerId;
            SpeakerSprite = data.SpeakerSprite;
            ItemId = data.ItemId;
        }
        
        public DialogueNode () {
    
        }
    }
}