using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BroDisplayManager : MonoBehaviour {
    [SerializeField] private List<BeefCake> beefCakes;

    [SerializeField] private MeshFilter broMeshFilter;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject strength;
    [SerializeField] private GameObject speed;
    [SerializeField] private GameObject stamina;

    [SerializeField] private Color statFilledCol;
    [SerializeField] private Color statEmptyCol;
    
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
        costText.text = "cost: " + beefCake.cost;

        // Update stats
        UpdateStat(strength, beefCake.strength);
        UpdateStat(stamina, beefCake.stamina);
        UpdateStat(speed, beefCake.speed);
    }

    // Displays stat level using the child images 
    private void UpdateStat(GameObject statObject, int level) {
        if (level < 0 || level > statObject.transform.childCount)
            Debug.LogError("BroDisplayManager: Stat level is invalid.");

        for (int i = 0; i < statObject.transform.childCount; i++) {
            GameObject child = statObject.transform.GetChild(i).gameObject;

            // TODO: This is not final. Will probably be changed to different sprites instead of colors.
            Image childImg = child.GetComponent<Image>();
            if (childImg == null)
                Debug.Log("BroDisplayManager: Image component not found on stat children.");

            if (i < level)
                childImg.color = statFilledCol;
            else
                childImg.color = statEmptyCol;
        }
    }
}
