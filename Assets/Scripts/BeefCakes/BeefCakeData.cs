using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeefCakeData", menuName = "ScriptableObjects/BeefCakeData", order = 1)]
public class BeefCakeData : ScriptableObject { 
    public string displayName;

    [Header("Stats")]
    public int strength;
    public int stamina;
    public int speed;

    public int level;
    public int cost;

    public int stageAvailable;

    public GameObject prefab;
}
