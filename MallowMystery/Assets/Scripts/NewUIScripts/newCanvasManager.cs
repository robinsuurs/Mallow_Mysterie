using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class newCanvasManager : MonoBehaviour//, IPointerClickHandler
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private JournalManager Journal;
    [SerializeField] private UnityEvent closeUI;
    

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
        Journal.activate();
    }

    public void DisableCanvas()
    {
        Journal.closeJournal();
        canvas.SetActive(false);
        closeUI.Invoke();
    }

    public void openJournalPage(UIPage page)
    {
        canvas.SetActive(true);
        if (Journal.isOpen && (page == Journal.currentPage || page == UIPage.Settings))
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