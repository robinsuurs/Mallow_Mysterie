using System;

namespace Dialogue.RunTime {
    [Serializable]
    public class ItemPortCombi {
        public string portname;
        public string itemName;

        public ItemPortCombi(string generatedPortPortName, string itemName) {
            this.portname = generatedPortPortName;
            this.itemName = itemName;
        }
    }
}