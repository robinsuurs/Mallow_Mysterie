using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private List<TabButt> tabButtons;
    private Color tabIdle;
    private Color tabHover;
    private Color tabActive;

    private TabButt selectedTab;

    private void Start()
    {
        tabIdle = new Color(1f, 0.6f, 0.6f);
        tabHover = new Color(1f, 0.4f, 0.4f);
        tabActive = new Color(1f, 0.8f, 0.8f);
        // resetTabs();

        foreach (TabButt butt in tabButtons)
        {
            butt.GetComponent<Image>().color = tabIdle;
            butt.deactivate();
        }
        OnTabSelected(tabButtons[0]);
        

    }


    public void Subscribe(TabButt button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButt>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButt button)
    {
        resetTabs();
        if (button != selectedTab)
        {
            button.GetComponent<Image>().color = tabHover;
        }
    }

    public void OnTabExit(TabButt button)
    {
        resetTabs();
    }
    public void OnTabSelected(TabButt button)
    {
        selectedTab?.deactivate();
        selectedTab = button;
        selectedTab.activate();
        resetTabs();
        button.GetComponent<Image>().color = tabActive;
    }

    void resetTabs()
    {
        foreach (TabButt butt in tabButtons)
        {
            if (butt != selectedTab)
            {
                butt.GetComponent<Image>().color = tabIdle;
            }
        }
    }
}
