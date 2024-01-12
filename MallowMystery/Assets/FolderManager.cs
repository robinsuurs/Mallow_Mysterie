using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class FolderManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> clues;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private GameObject folder;
    [SerializeField] private GameEventStandardAdd folderPickUpEvent;
    private InputAction moveAction;
    private InputAction interactAction;
    private bool pickup;
    
    private void Awake()
    {
        moveAction = input.FindAction("Move");
        interactAction = input.FindAction("Interact");
    }
    public void openFolder(){
        folder.SetActive(true);
        deactivateInput();
        foreach (GameObject clue in clues)
        {
            clue.GetComponent<ItemLoadScene>().ShowObjectOrNot();
        }
    }
    public void closeFolder(){
        activateInput();
        
        if (clues.All(clues => !clues.activeSelf)) {
            folderPickUpEvent.Raise();
        }
        
        folder.SetActive(false);
    }
    
    public void activateInput() {moveAction.Enable(); interactAction.Enable();}
    public void deactivateInput() {moveAction.Disable(); interactAction.Disable();}
}
