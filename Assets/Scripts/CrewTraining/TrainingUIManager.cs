using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingUIManager : MonoBehaviour {
    
    [Header("UI Fields")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    // TODO: add XP display

    [SerializeField] private TMP_Text statValue;
    [SerializeField] private GameObject staminaDisplay;
    [SerializeField] private GameObject strengthDisplay;
    [SerializeField] private GameObject speedDisplay;

    [SerializeField] private GameObject charModel;
    [SerializeField] private List<GameObject> increaseEffects;

    public void OnStart(CrewBeefCake beefCake) {
        SetTextUI(beefCake);
        InstantiateModel(beefCake);
    }

    public void NotifyStatTrained(CrewBeefCake beefCake) {
        SetStatText(beefCake);
        ShowTrainEffects();

        // TODO: Animate the character model.
    }

    private void SetTextUI(CrewBeefCake beefCake) {
        nameText.text = beefCake.displayName;
        levelText.text = beefCake.level.ToString();

        SetStatText(beefCake);

        // TODO: Implement showing the chosen stat. For now only show strength.
    }

    private void SetStatText(CrewBeefCake beefCake) {
        statValue.text = beefCake.strength.ToString();
    }

    private void ShowTrainEffects() {
        foreach (GameObject effect in increaseEffects)
            effect.SetActive(true);
    }

    #region Character Model methods
    private void InstantiateModel(CrewBeefCake beefCake) {
        // Instantiate new character model as a child of {charModel}
        GameObject model = Instantiate(beefCake.characterPrefab);
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
