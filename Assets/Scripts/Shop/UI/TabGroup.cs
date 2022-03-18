using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages a group of TabButtons and defines interactions for them
// Order of {contentObjects} must match order of TabButton objects in parent!
public class TabGroup : MonoBehaviour {
    [SerializeField] private Color idleColor;
    [SerializeField] private Color selectColor;
    [SerializeField] private List<GameObject> contentObjects;

    private List<TabButton> tabButtons;
    
    // Adds a TabButton to the TabGroup list
    public void Subscribe(TabButton button) {
        if (tabButtons == null) tabButtons = new List<TabButton>();

        tabButtons.Add(button);
    }


    #region TabButton interactions
    public void OnTabSelected(TabButton button) {
        // Set the color of the selected tab only to {selectColor}
        ResetTabs();
        button.text.color = selectColor;
        button.Select();

        // Set the content of the tab to match the selected TabButton
        ResetContent();
        int index = button.transform.GetSiblingIndex();
        contentObjects[index].SetActive(true);
    }

    private void ResetTabs() {
        foreach (TabButton button in tabButtons) {
            button.text.color = idleColor;
            button.Deselect();
        }
    }

    private void ResetContent() {
        foreach (GameObject content in contentObjects) {
            content.gameObject.SetActive(false);            
        }
    }
    #endregion
}
