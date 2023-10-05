namespace Subtegral.DialogueSystem.DataContainers
{
    [System.Serializable]
    public class ExposedProperty
    {
        public static ExposedProperty CreateInstance()
        {
            return new ExposedProperty();
        }
        
        public static ExposedProperty CreateInstancePlaceHolder()
        {
            
            return new ExposedProperty() {
                PropertyName = "Ignore"
            };
        }

        public string PropertyName = "New String";
        public string PropertyValue = "New Value";
    }
}