using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds static information about a Car type
[CreateAssetMenu(fileName = "CarTypeData", menuName = "ScriptableObjects/CarTypeData", order = 1)]
public class CarTypeData : ScriptableObject {
    // Car stages
    public GameObject[] carStates = new GameObject[3];

    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

    [Header("Display Fields")]
    public Sprite carshot;
    public Color backgroundColor;
}
