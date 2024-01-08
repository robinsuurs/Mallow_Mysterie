using System.Collections.Generic;

namespace Dialogue.Editor.Graph {
    public class Questions {
        public string question;
        public string questionUID;

        public Questions(string question, string questionUid) {
            this.question = question;
            this.questionUID = questionUid;
        }
    }
}