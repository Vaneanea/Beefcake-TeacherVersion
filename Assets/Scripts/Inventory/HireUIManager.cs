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
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text costText;

    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text speedText;

    [SerializeField] TMP_Text hireText;
    [SerializeField] GameObject hireButtonEnabled;
    [SerializeField] GameObject hireButtonDisabled;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject leftArrow;

    [SerializeField] GameObject charModel;

    #region Display methods (called by CrewShopInventory by UI elements)
    public void DisplayCurrentItem(ShopBeefCake curItem) {
        hireButtonEnabled.SetActive(true);
        hireButtonDisabled.SetActive(false);
        hireButtonEnabled.GetComponent<Button>().interactable = true;

        SetTextUI(curItem);
        SetCharacterModel(curItem);
    }

    public void OnClickHire() {
        hireButtonDisabled.SetActive(true);
        hireButtonEnabled.SetActive(false);
    }

    public void DisableArrows() {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }
    #endregion

    private void SetTextUI(ShopBeefCake item) {
        BeefCakeData source = item.source;

        nameText.text = source.displayName;
        levelText.text = source.level.ToString();
        costText.text = source.cost.ToString();
        hireText.text = "Hire " + source.displayName.ToString();

        staminaText.text = item.stamina.ToString();
        strengthText.text = item.strength.ToString();
        speedText.text = item.speed.ToString();
    }

    #region Character Model methods
    private void SetCharacterModel(ShopBeefCake item) {
        BeefCakeData source = item.source;

        // Destroy any already existing children of {charModel} object
        foreach (Transform child in charModel.transform) {
            Destroy(child.gameObject);
        }

        InstantiateModel(source.prefab);
    }

    private void InstantiateModel(GameObject prefab) {
        // Instantiate new character model as a child of {charModel}
        GameObject model = Instantiate(prefab);
        model.transform.parent = charModel.transform;
        
        // Set its local transform to display it properly 
        model.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Change {model} layer to UI (including its children)
        model.layer = LayerMask.NameToLayer("UI");
        foreach (Transform trans in model.GetComponentsInChildren<Transform>()) {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }
    #endregion
}
