using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeefCakeData", menuName = "ScriptableObjects/BeefCakeData", order = 1)]
public class BeefCakeData : ScriptableObject { 
    public string displayName;

    [Header("Stat Ranges")]
    public Vector2Int strengthRange;
    public Vector2Int staminaRange;
    public Vector2Int speedRange;

    public int level;

    public int stageAvailable;

    [Header("Display fields")]
    public Sprite headshot;
    public GameObject characterPrefab;
    public GameObject shopPose;
    public Color bgColor;
}
