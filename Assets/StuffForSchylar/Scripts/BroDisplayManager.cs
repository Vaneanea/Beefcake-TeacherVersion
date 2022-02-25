using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BroDisplayManager : MonoBehaviour {
    [SerializeField] private List<BeefCake> beefCakes;

    [SerializeField] private MeshFilter broMeshFilter;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text costText;
    
    private int curBro = 0;

    private void Start() {
        DisplayCurrentBro();
    }

    public void OnClickNext() {
        curBro = (curBro + 1) % beefCakes.Count;
        
        DisplayCurrentBro();
    }

    public void OnClickPrevious() {
        curBro--;
        if (curBro < 0)
            curBro = beefCakes.Count - 1;

        DisplayCurrentBro();
    }

    private void DisplayCurrentBro() {
        BeefCake curBeefCake = beefCakes[curBro];

        broMeshFilter.mesh = curBeefCake.mesh;
        UpdateUI(curBeefCake);
    }

    private void UpdateUI(BeefCake beefCake) {
        nameText.text = beefCake.broName;
        levelText.text = "level: " + beefCake.level;

        strengthText.text = "strength: " + beefCake.strength;
        speedText.text = "speed: " + beefCake.speed;
        staminaText.text = "stamina: " + beefCake.stamina;

        costText.text = "cost: " + beefCake.cost;
    }
}
