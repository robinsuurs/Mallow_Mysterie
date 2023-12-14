using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class newCanvasManager : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] private GameObject Journal;
    [SerializeField] private GameObject JournalTabs;
    [SerializeField] private List<GameObject> pages;
    [SerializeField] private GameEventStandardAdd closeUI;

    [SerializeField] private InputActionAsset input;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void OnPointerClick(PointerEventData eventData) //TODO: Doet het niet.
    // {
    //     closeJournal();
    // }

    public void closeJournal()
    {
        if (Journal.activeSelf)
        {
            Journal.SetActive(false);
            foreach (var page in pages)
            {
                page.SetActive(false);
            }
            input.Enable();
            closeUI.Raise();
        }
    }



    public void openJournal()
    {
        input.Disable();
        Journal.SetActive(true);
        // JournalTabs.
        //Todo, add open to specific tab functionality
    }
}

