using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Holds an instance of a BeefCakeData ScriptableObject with extra stuff for shop items
// Contains logic for player interaction and buying item
public class ShopBeefCake {
    public BeefCakeData source;

    public int strength;
    public int stamina;
    public int speed;

    public bool isAvailable;

    public ShopBeefCake(BeefCakeData source) {
        this.source = source;

        SetStats();
        isAvailable = true; // TODO: set isAvailable based on current stage
    }

    // Randomize stats for concrete ShopBeefCake instance
    private void SetStats() {
        strength = Random.Range(source.strengthRange.x, source.strengthRange.y);
        stamina = Random.Range(source.staminaRange.x, source.staminaRange.y);
        speed = Random.Range(source.speedRange.x, source.speedRange.y);
    }

    #region Item Buy Methods
    public void OnItemBuy() {
        CrewInventory.instance.Add(this);
        isAvailable = false;
    }
    #endregion   
}
