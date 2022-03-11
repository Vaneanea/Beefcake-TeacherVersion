using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Holds an instance of a BeefCakeData ScriptableObject
// with extra data and methods related to crew management
[Serializable]
public class CrewBeefCake {
    [SerializeField] private string broName;

    private int stamina;
    private int strength;
    private int speed;

    public CrewBeefCake(BeefCakeData source) {
        stamina = source.stamina;
        strength = source.strength;
        speed = source.speed;

        broName = source.displayName;
    }

    // TODO: add other methods to modify CrewBeefCake 
}
