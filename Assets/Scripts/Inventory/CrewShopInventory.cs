using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handle shop management and notify UIManager of changes
public class CrewShopInventory : MonoBehaviour {
    [SerializeField] private List<BeefCakeData> shopItemsSource;
    private List<ShopBeefCake> shopItems;

    private HireUIManager uiManager;
    private int curItem;
    private int availableCount; // TODO: This var needs to be updated properly

    private void Awake() {
        shopItems = new List<ShopBeefCake>();
    }

    private void Start() {
        uiManager = GetComponent<HireUIManager>();
        
        MakeShop();

        NotifyCurItemChange();
    }

    #region Display methods
    // Attached to right arrow
    public void OnClickRight() {
        // Find the first available item going forwards
        do {
            curItem = (curItem + 1) % shopItems.Count;
        } while (!shopItems[curItem].isAvailable);

        NotifyCurItemChange();
    }

    // Attached to left arrow
    public void OnClickLeft() {
        // Find the first available going backwards
        do {
            curItem--;
            if (curItem < 0)
                curItem = shopItems.Count - 1;
        } while (!shopItems[curItem].isAvailable);

        NotifyCurItemChange();
    }

    // Attached to hire button
    public void OnClickHire() {
        shopItems[curItem].OnItemBuy();
        availableCount--;

        uiManager.OnClickHire();
    }

    private void NotifyCurItemChange() {
        if (availableCount == 1)
            uiManager.DisableArrows();

        uiManager.DisplayCurrentItem(shopItems[curItem]);
    }
    #endregion

    // Construct shop items from the source data: {shopItemsSource}
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
}
