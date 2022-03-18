using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// Delegates user interaction to TabGroup that is subscribed to
public class TabButton : MonoBehaviour {
    [SerializeField] private TabGroup tabGroup;

    [HideInInspector] public TMP_Text text;

    public void OnClick() {
        tabGroup.OnTabSelected(this);
    }

    private void Start() {
        text = GetComponentInChildren<TMP_Text>();

        tabGroup.Subscribe(this);   
    }
}
