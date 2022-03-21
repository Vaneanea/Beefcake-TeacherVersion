using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// Holds an instance of a BeefCakeData ScriptableObject
// with extra data and methods related to crew management
[Serializable]
public class CrewBeefCake : ScriptableObject
{

    public BeefCakeData source;

    public string displayName;

    public int speed;
    public int strength;
    public int stamina;

    public GameObject characterPrefab;
    public Sprite headshot;
    public Color bgColor;

    public int currentStamina;
    public bool isFatigued;
    public bool isPlayer;



}
