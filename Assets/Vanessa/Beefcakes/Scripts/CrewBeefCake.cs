using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// Holds an instance of a BeefCakeData ScriptableObject
// with extra data and methods related to crew management
[Serializable]
public class CrewBeefCake : ScriptableObject
{
    public BeefCakeData source { get; set; }

    public string displayName { get; set; }

    public int speed { get; set; }
    public int strength { get; set; }
    public int stamina { get; set; }

    public GameObject characterPrefab { get; set; }
    public Sprite headshot { get; set; }
    public Color bgColor { get; set; }

    public int currentStamina { get;  set; }
    public bool isFatigued { get;  set; }
    public bool isPlayer { get; set; }



}
