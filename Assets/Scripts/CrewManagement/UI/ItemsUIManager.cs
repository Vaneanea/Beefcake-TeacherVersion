using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUIManager : MonoBehaviour {
    [SerializeField] private GameObject chooseItemPopup;
    [SerializeField] private GameObject characterModel;

    private void Start() {
        // TODO: Go through EquipSlot array and display CrewBeefCake equipped items.         
    }

    public void OnEquipSlotClick() {
        ShowChoosePopup(true);
    }

    public void OnCloseButtonClick() {
        ShowChoosePopup(false);
    }

    private void ShowChoosePopup(bool show) {
        chooseItemPopup.SetActive(show);
        characterModel.SetActive(!show);
    }
}
