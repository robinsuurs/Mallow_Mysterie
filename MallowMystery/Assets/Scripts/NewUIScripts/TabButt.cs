using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum JournalPage
{
    Boeiah,
    Clues,
    Settings, 
    Deduction,
    Map
}
public class TabButt : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    private Image background;
    [SerializeField] [CanBeNull] private GameObject Page = null;
    [SerializeField] private Color Idle;
    [SerializeField] private Color Hover;
    [SerializeField] private Color Active;
    private Image myImage;
    [SerializeField] public JournalPage Me;
    

    private void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
        myImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void activate()
    {
        if (Page)
        {
            Page.SetActive(true);
        }
    }
    public void deactivate()
    {
        // print("pageDeactivate");
        if (Page)
        {
            Page.SetActive(false);
        }
    }

    public void ColorIdle()
    {
        myImage.color = Idle;
    }
    public void ColorHover()
    {
        myImage.color = Hover;
    }
    public void ColorActive()
    {
        myImage.color = Active;
    }
}
