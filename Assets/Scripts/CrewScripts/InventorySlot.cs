using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles Inventory Slot display
public class InventorySlot : MonoBehaviour {
    // TODO: add XP display

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text speedText;

    [SerializeField] private Image headshot;
    [SerializeField] private Image bottomGlow;
    [SerializeField] private Image backGlow;
    [SerializeField] private Image background;

    public void Set(CrewBeefCake crewItem) {
        //BeefCakeData source = crewItem.source;

        headshot.sprite = crewItem.headshot;

        background.color = crewItem.bgColor;
        bottomGlow.color = crewItem.bgColor;
        backGlow.color = crewItem.bgColor;

        levelText.text = crewItem.level.ToString();
        speedText.text = crewItem.speed.ToString();

        gameObject.name = crewItem.displayName + " Slot";
    }
}
