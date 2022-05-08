using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WashUIManager : MonoBehaviour {
    [SerializeField] private SliderScript progressBar;
    [SerializeField] private TMP_Text debugText;

    public void SetProgressMaxValue(int value) {
        progressBar.SetMaxValue(value);
    }

    public void SetProgressValue(int value) {
        progressBar.SetValue(value);
    }

    public void SetDebugText(string text) {
        debugText.text = text;
    }
}
