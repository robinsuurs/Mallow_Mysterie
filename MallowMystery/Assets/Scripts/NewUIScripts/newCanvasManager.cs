using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class newCanvasManager : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private JournalManager Journal;
    [SerializeField] private GameEventStandardAdd closeUI;
    
    [SerializeField] private UIControls ui;

    private void Start()
    {
        deactivateCanvas();
    }

    public void activateCanvas(){ canvas.SetActive(true);}
    public void deactivateCanvas(){ canvas.SetActive(false);}
    public bool isCanvasActive(){ return canvas.activeSelf; }
    public void openJournal()
    {
        canvas.SetActive(true);
        ui.deactivateInput();
        Journal.activate();
    }

    public void DisableCanvas()
    {
        Journal.closeJournal();
        ui.activateInput();
        canvas.SetActive(false);
        closeUI.Raise();
    }

    public void openJournalPage(UIPage page)
    {
        canvas.SetActive(true);
        if (Journal.isOpen && Journal.currentPage == page)
        {
            DisableCanvas();
            return;
        }
        openJournal();
        Journal.selectPage(page);
    }
    
}

public enum UIPage
{
    Clues,
    Settings, 
    Deduction,
    Map,
    Folder,
    Pinboard
}