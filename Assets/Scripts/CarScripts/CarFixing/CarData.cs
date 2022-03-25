using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car")]
public class CarData : ScriptableObject
{
    [Header("Display Values")]
    public Sprite carshot;

    [Header("Gameplay Values")]
    public int starCount; // Same as difficulty

    //car stages
    public GameObject[] carStates = new GameObject[3];

    public int firstStageHitsNeeded, secondStageHitsNeeded;

    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

    // TODO: Add constructor-like method that initializes all the stats based on {starCount}
}