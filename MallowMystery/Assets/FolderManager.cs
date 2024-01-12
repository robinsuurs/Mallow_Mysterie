using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FolderManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> clues;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private GameObject folder;
    private InputAction moveAction;
    private InputAction interactAction;
    private bool pickup;
    
    private void Awake()
    {
        // clues = ;
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
        pickup = true;
        foreach (GameObject clue in clues)
        {
            if (clue.activeSelf) pickup = false;
            print(clue.activeSelf);
            
        }
        folder.SetActive(false);
        activateInput();

        if (pickup) folder.GetComponent<ItemLoadScene>().InteractWithItem();
    }
    public void activateInput() {moveAction.Enable(); interactAction.Enable();}
    public void deactivateInput() {moveAction.Disable(); interactAction.Disable();}
}
