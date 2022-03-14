using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Handle Crew Shop UI and display
public class HireUIManager : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text speedText;

    [SerializeField] GameObject hireButton;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject leftArrow;

    public void DisplayCurrentItem(ShopBeefCake curItem) {
        hireButton.GetComponent<Button>().interactable = true;
        hireButton.GetComponentInChildren<TMP_Text>().text = "HIRE";

        SetTextUI(curItem);
    }

    public void OnClickHire() {
        hireButton.GetComponent<Button>().interactable = false;
        hireButton.GetComponentInChildren<TMP_Text>().text = "HIRED!";
    }

    public void DisableArrows() {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }

    private void SetTextUI(ShopBeefCake item) {
        BeefCakeData source = item.source;

        nameText.text = source.displayName;
        staminaText.text = source.stamina.ToString();
        strengthText.text = source.strength.ToString();
        speedText.text = source.speed.ToString();
    }
}
