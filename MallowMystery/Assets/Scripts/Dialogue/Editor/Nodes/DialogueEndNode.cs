using Dialogue.RunTime;
using UnityEditor.Experimental.GraphView;

namespace Dialogue.Editor.Nodes {
    public class DialogueEndNode : Node
    {
        public string DialogueText;
        public string GUID;
        public bool EntyPoint = false;
        
        public DialogueEndNode (DialogueEndNodeData data) {
            DialogueText = data.dialogueText;
            GUID = data.nodeGuid;
        }
        public DialogueEndNode() {
        
        }
    }
}
