using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages a group of TabButtons and defines interactions for them
public class TabGroup : MonoBehaviour {
    [SerializeField] private Color idleColor;
    [SerializeField] private Color selectColor;

    private List<TabButton> tabButtons;

    // Adds a TabButton to the TabGroup list
    public void Subscribe(TabButton button) {
        if (tabButtons == null) tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }


    #region TabButton interactions
    public void OnTabSelected(TabButton button) {
        ResetTabs();
        button.text.color = selectColor;
    }

    private void ResetTabs() {
        foreach (TabButton button in tabButtons) {
            button.text.color = idleColor;
        }
    }
    #endregion
}
