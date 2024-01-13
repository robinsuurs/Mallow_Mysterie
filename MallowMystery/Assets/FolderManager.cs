using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class FolderManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> clues;
    [SerializeField] private GameObject folder;
    [SerializeField] private GameEventStandardAdd folderPickUpEvent;
    [SerializeField] private GameEventStandardAdd openUIElement;
    [SerializeField] private GameEventStandardAdd closeUIElement;
    
    public void openFolder(){
        folder.SetActive(true);
        foreach (GameObject clue in clues)
        {
            clue.GetComponent<ItemLoadScene>().ShowObjectOrNot();
        }
        openUIElement.Raise();
    }
    public void closeFolder(){
        
        if (clues.All(clues => !clues.activeSelf)) {
            folderPickUpEvent.Raise();
        }
        closeUIElement.Raise();
        folder.SetActive(false);
    }
}
