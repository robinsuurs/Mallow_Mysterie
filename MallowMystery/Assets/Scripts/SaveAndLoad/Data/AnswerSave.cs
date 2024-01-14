namespace SaveAndLoad.Data {
    [System.Serializable]
    public class AnswerSave {

        public string UID;
        public bool enabled;

        public AnswerSave(string UID, bool enabled) {
            this.UID = UID;
            this.enabled = enabled;
        }
    }
}