using System;

namespace Dialogue.RunTime {
    [Serializable]
    public class QuestionAnswerPortCombi {
        public string portname;
        public string questionUID;
        public string answerUID;

        public QuestionAnswerPortCombi(string generatedPortPortName, string questionUid, string answerUid) {
            this.portname = generatedPortPortName;
            this.questionUID = questionUid;
            this.answerUID = answerUid;
        }
        
        public QuestionAnswerPortCombi(string questionUid, string answerUid) {
            this.questionUID = questionUid;
            this.answerUID = answerUid;
        }
    }
}