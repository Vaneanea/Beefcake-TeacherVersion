using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Handles Shop management
// TODO: Handle populating the different tabs with correct shop items
public class ShopManager : MonoBehaviour {
    [Header("Display Variables")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject itemGroup;


    private List<ItemData> shopItemSources;
    private List<ShopItem> shopItems; // TODO: Take into account item Type

    // Start is called before the first frame update
    void Start() {
        MakeShop();
    }
    
    private void MakeShop() {
        // Populate {shopItemSources} with all ItemData objects 
        shopItemSources = Resources.LoadAll<ItemData>("Item Data").ToList();

        // Add a shop slot for each item in {shopItemSources}
        foreach (ItemData source in shopItemSources) {
            GameObject obj = Instantiate(itemSlotPrefab);
            obj.transform.SetParent(itemGroup.transform, false);

            ShopItem shopItem = obj.GetComponent<ShopItem>();
            shopItem.Set(source);
        }
    }

}
