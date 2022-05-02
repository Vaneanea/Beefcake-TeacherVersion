using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashUIManager : MonoBehaviour {
    [SerializeField] private SliderScript progressBar;

    public void SetProgressMaxValue(int value) {
        progressBar.SetMaxValue(value);
    }

    public void SetProgressValue(int value) {
        progressBar.SetValue(value);
    }
}
