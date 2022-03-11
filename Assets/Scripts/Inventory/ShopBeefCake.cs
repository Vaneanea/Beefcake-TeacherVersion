using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Holds an instance of a BeefCakeData ScriptableObject with extra stuff for shop items
// Handles UI Display
// Contains logic for player interaction and buying item
public class ShopBeefCake {
    public BeefCakeData source;
    
    public bool isAvailable;

    public ShopBeefCake(BeefCakeData source) {
        this.source = source;
        isAvailable = true;
    }

    #region Item Buy Methods
    // TODO: Figure out when to call this
    private void OnItemBuy() {
        CrewInventory.instance.Add(source);
        isAvailable = false;

        // TODO: Notify CrewShopInventory or something to not display item?
        //       ? maybe Destroy(this) ?
    }
    #endregion

    
}
