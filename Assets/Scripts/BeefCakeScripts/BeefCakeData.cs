using System;
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

    [Header("Visual Prefab ID")]
    public int visualID;
  
}
