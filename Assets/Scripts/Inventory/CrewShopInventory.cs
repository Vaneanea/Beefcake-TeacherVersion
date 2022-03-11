using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Handle shop management and display
public class CrewShopInventory : MonoBehaviour {
    [SerializeField] private List<BeefCakeData> shopItemsSource;
    private List<ShopBeefCake> shopItems;

    [Header("UI Variables")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text staminaText;
    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text speedText;

    private void Awake() {
        shopItems = new List<ShopBeefCake>();
    }

    private void Start() {
        MakeShop();
    }

    private void DrawShop() {
        if (shopItems.Count > 0)
            DisplayItem(shopItems[0]);
    }

    // Construct shop items
    // TODO: only called ONCE at the first start of the game, persistent throughout
    private void MakeShop() {
        foreach (BeefCakeData source in shopItemsSource) {
            ShopBeefCake shopItem = new ShopBeefCake(source);
            shopItems.Add(shopItem);
        }
    }

    #region UI Methods

    private void DisplayItem(ShopBeefCake item) {
        // TODO: Set this up in editor and hook it to UI
        SetTextUI(item);
    }

    private void SetTextUI(ShopBeefCake item) {
        BeefCakeData source = item.source;

        nameText.text = source.displayName;
        staminaText.text = source.stamina.ToString();
        strengthText.text = source.strength.ToString();
        speedText.text = source.speed.ToString();
    }
    #endregion
}
