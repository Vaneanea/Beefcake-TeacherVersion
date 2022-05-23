using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Handles Shop management
// TODO: Handle populating the different tabs with correct shop items
public class ShopManager : MonoBehaviour {
    [Header("Display Variables")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject contentParent; 

    private List<ItemData> shopItemSources;
    private List<ShopItem> shopItems; // TODO: Take into account item Type

    void Start() {
        shopItems = new List<ShopItem>();

        MakeShop();
    }
    
    private void MakeShop() {
        // Populate {shopItemSources} with all ItemData objects 
        shopItemSources = Resources.LoadAll<ItemData>("Data/Shop").ToList();

        // Add a shop slot for each item in {shopItemSources}
        foreach (ItemData source in shopItemSources) {
            string childName = source.type.ToString() + "_Content";
            GameObject contentTab = contentParent.transform.Find(childName).gameObject;

            GameObject obj = Instantiate(itemSlotPrefab);
            obj.transform.SetParent(contentTab.transform, false);

            ShopItem shopItem = obj.GetComponent<ShopItem>();
            shopItem.Set(source);

            shopItems.Add(shopItem);
        }
    }
}
