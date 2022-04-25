using UnityEngine;
using System;

[Serializable]
public class CrewBeefCake : ScriptableObject
{
    public int level;

    public string displayName;

    public int speed;
    public int strength;
    public int stamina;

    public int visualID;

    public GameObject characterPrefab;
    public Sprite headshot;
    public Color bgColor;

    public int currentStamina;
    public bool isFatigued = false;
    public bool isPlayer = false;
}
