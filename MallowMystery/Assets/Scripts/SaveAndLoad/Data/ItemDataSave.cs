

[System.Serializable]
public class ItemDataSave
{
    public string itemName;
    public bool hasBeenPickedUp = false;
    public int pickedUpNumber;

    public ItemDataSave(string itemName, bool hasBeenPickedUp, int pickedUpNumber) {
        this.itemName = itemName;
        this.hasBeenPickedUp = hasBeenPickedUp;
        this.pickedUpNumber = pickedUpNumber;
    }
}
