using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] GameObject hireButton;
    [SerializeField] GameObject nextArrow;
    [SerializeField] GameObject previousArrow;

    private int curItem;
    [SerializeField] private int availableCount; // TODO: This var needs to be updated properly

    private void Awake() {
        shopItems = new List<ShopBeefCake>();
    }

    private void Start() {
        MakeShop();

        DisplayCurrentItem();
    }

    // Construct shop items
    // TODO: only called ONCE at the first start of the game, persistent throughout
    private void MakeShop() {
        availableCount = 0;

        // Create ShopBeefCake objects
        foreach (BeefCakeData source in shopItemsSource) {
            ShopBeefCake shopItem = new ShopBeefCake(source);
            shopItems.Add(shopItem);

            if (shopItem.isAvailable)
                availableCount++;
        }

        // Determine which item to display based on available items
        for(int index = 0; index < shopItems.Count; index++) 
            if (shopItems[index].isAvailable) {
                curItem = index;
                break;
            }   
    }

    private void OnHireItem() {
        shopItems[curItem].OnItemBuy();
        availableCount--;
    }

    #region UI Methods

    public void OnClickNext() {
        // Find the first available item going forwards
        do {
            curItem = (curItem + 1) % shopItems.Count;
        } while (!shopItems[curItem].isAvailable);
        

        DisplayCurrentItem();
    }

    public void OnClickPrevious() {
        // Find the first available going backwards
        do {
            curItem--;
            if (curItem < 0)
                curItem = shopItems.Count - 1;
        } while (!shopItems[curItem].isAvailable);
        

        DisplayCurrentItem();
    }

    public void OnClickHire() {
        OnHireItem();

        hireButton.GetComponent<Button>().interactable = false;
        hireButton.GetComponentInChildren<TMP_Text>().text = "HIRED!";
    }

    private void DisplayCurrentItem() {
        if (availableCount == 1)
            DisableArrows();

        hireButton.GetComponent<Button>().interactable = true;
        hireButton.GetComponentInChildren<TMP_Text>().text = "HIRE";

        SetTextUI(shopItems[curItem]);
    }

    private void SetTextUI(ShopBeefCake item) {
        BeefCakeData source = item.source;

        nameText.text = source.displayName;
        staminaText.text = "Stamina: " + source.stamina.ToString();
        strengthText.text = "Strength: " + source.strength.ToString();
        speedText.text = "Speed: " + source.speed.ToString();
    }

    private void DisableArrows() {
        nextArrow.SetActive(false);
        previousArrow.SetActive(false);
    }
    #endregion
}
