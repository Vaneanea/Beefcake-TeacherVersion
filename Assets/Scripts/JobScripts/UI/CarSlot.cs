using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles UI display of a CarTypeData object
public class CarSlot : MonoBehaviour {
    [SerializeField] private Image backgroundImg;
    [SerializeField] private Image frameImg;
    [SerializeField] private Image glowImg;
    [SerializeField] private Image backGlowImg;

    [SerializeField] private Image carImg;
    [SerializeField] private GameObject starParent;

    [Header("Image Display Values")]
    [SerializeField] private float darkMod;
    [SerializeField] private float lightMod;
    [SerializeField] private float glowAlpha;

    public void Set(CarTypeData source, int starCount) {
        SetImageColors(source.backgroundColor);

        carImg.sprite = source.carshot;

        SetStarCount(starCount);
    }

    private void SetImageColors(Color mainColor) {
        backgroundImg.color = mainColor;
        frameImg.color = new Color(mainColor.r * darkMod, mainColor.g * darkMod, mainColor.b * darkMod, 1);
        glowImg.color = new Color(mainColor.r, mainColor.g, mainColor.b, glowAlpha);
        backGlowImg.color = new Color(mainColor.r * lightMod, mainColor.g * lightMod, mainColor.b * lightMod, 1);
    }

    private void SetStarCount(int starCount) {
        int maxStarCount = starParent.transform.childCount;

        for (int index = 0; index < maxStarCount; index++) {
            Transform starChild = starParent.transform.GetChild(index);
            Transform starActive = starChild.transform.GetChild(0);

            bool isActive = (index < starCount);
            starActive.gameObject.SetActive(isActive);
        }
    }
}
