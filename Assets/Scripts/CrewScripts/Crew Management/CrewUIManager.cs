using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrewUIManager : MonoBehaviour {
    [Header("UI Variables")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;

    // Stats NEED to be placed in list in order: strength, speed, stamina
    [SerializeField] List<TMP_Text> statsText;
    [SerializeField] List<TMP_Text> costText;

    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject leftArrow;

    [SerializeField] GameObject charModel;

    #region Display methods (called by CrewInventoryDisplay and by UI elements)
    public void DisplayCurrentItem(CrewBeefCake curItem) {
        SetTextUI(curItem);
        SetCharacterModel(curItem);
    }

    public void DisableArrows() {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }
    #endregion

    private void SetTextUI(CrewBeefCake item) {
        nameText.text = item.displayName;
        levelText.text = item.level.ToString();

        // For now, all stats have the same hard-coded training cost.
        foreach (TMP_Text text in costText)
            text.text = "250";
        
        statsText[0].text = item.strength.ToString("n0");
        statsText[1].text = item.speed.ToString("n0");
        statsText[2].text = item.stamina.ToString("n0");
    }

    #region Character Model methods
    private void SetCharacterModel(CrewBeefCake item) {
        // Destroy any already existing children of {charModel} object
        foreach (Transform child in charModel.transform) {
            Destroy(child.gameObject);
        }

        BeefCakeVisualData visualData = Resources.Load<BeefCakeVisualData>("Data/BeefCakeVisualData/" + item.displayName.ToString());

        InstantiateModel(visualData.characterPrefab);
    }

    private void InstantiateModel(GameObject prefab) {
        // Instantiate new character model as a child of {charModel}
        GameObject model = Instantiate(prefab);
        model.transform.parent = charModel.transform;

        // Set its local transform to display it properly 
        model.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        // Change {model} layer to UI (including its children)
        model.layer = LayerMask.NameToLayer("UI");
        foreach (Transform trans in model.GetComponentsInChildren<Transform>()) {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }
    #endregion
}
