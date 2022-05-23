using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Items, Gym, Equipment }

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject {
    public string displayName;

    public Type type;

    [Header("Stat Modifiers")]
    public int strength;
    public int stamina;
    public int speed;

    public int cost;

    [Header("Display fields")]
    public Sprite icon;
}