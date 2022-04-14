using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Holds an instance of a BeefCakeData ScriptableObject with extra stuff for shop items
// Contains logic for player interaction and buying item
public class ShopBeefCake {
    public BeefCakeData source;

    public bool isAvailable;

    [Header("Concrete stats")]
    public int strength;
    public int stamina;
    public int speed;
    public int level;

    [Header("Display Prefab ID")]
    public int visualID;

    public int cost;
    private int costMod = 15;

    

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
        level = source.level;
        visualID = source.visualID;
        cost = (strength + speed + stamina) * costMod;
    }

    #region Item Buy Methods
    public void OnItemBuy() {
        CrewInventory.instance.Add(this);
        isAvailable = false;
    }
    #endregion   
}
