using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class JournalManager : MonoBehaviour
{
    
    [FormerlySerializedAs("canvas")] [SerializeField] private newCanvasManager canvasManager;
    [SerializeField] private GameObject Journal;

    [SerializeField] private List<GameObject> pages;
    [SerializeField] private TabButt cluesButton;
    [SerializeField] private TabButt deductionButton;
    [SerializeField] private TabButt mapButton;
    [SerializeField] private TabButt settingsButton;
    [SerializeField] private GameEventStandardAdd openUIElement;
    [SerializeField] private GameEventStandardAdd closeUIElement;
    public bool isOpen = false;
    public UIPage currentPage;

    
    public void closeJournal()
    {
        if (canvasManager.isCanvasActive())
        {
            canvasManager.activateCanvas();
            foreach (var page in pages)
            {
                page.SetActive(false);
            }

            if (isOpen) {
                closeUIElement.Raise();
            }
            isOpen = false;
        }
        Journal.SetActive(false);
    }
    
    public void selectPage(UIPage page)
    {
        activate();
        currentPage = page;
        if (page == UIPage.Clues) cluesButton.selectMe();
        else if (page == UIPage.Deduction) deductionButton.selectMe();
        else if (page == UIPage.Map) mapButton.selectMe();
        else if (page == UIPage.Settings) settingsButton.selectMe();
        else throw new Exception("UI page not availible or invalid!");
    }

    public void activate()
    {
        Journal.SetActive(true);
        if (!isOpen) {
            openUIElement.Raise();
        }
        isOpen = true;
    }
}
