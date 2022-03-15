using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Holds an instance of a BeefCakeData ScriptableObject
// with extra data and methods related to crew management
[Serializable]
public class CrewBeefCake {
    public BeefCakeData source;

    [SerializeField] private string displayName;

    private int stamina;
    private int strength;
    private int speed;

    public CrewBeefCake(ShopBeefCake data) {
        source = data.source;

        stamina = data.stamina;
        strength = data.strength;
        speed = data.speed;

        displayName = source.displayName;
    }

    // TODO: add other methods to modify CrewBeefCake 
}
