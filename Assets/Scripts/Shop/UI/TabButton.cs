using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

// Delegates user interaction to TabGroup that is subscribed to
// Notifies attached objects of selection and deselection using UnityEvents
public class TabButton : MonoBehaviour {
    [SerializeField] private TabGroup tabGroup;
    [HideInInspector] public TMP_Text text;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    public void OnClick() {
        tabGroup.OnTabSelected(this);
    }

    private void Start() {
        text = GetComponentInChildren<TMP_Text>();

        tabGroup.Subscribe(this);   
    }

    public void Select() {
        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Deselect() {
        if (onTabDeselected != null)
            onTabDeselected.Invoke();
    }
}
