using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using UnityEditor;

// Holds a concrete instance of CarTypeData 
public class ConcreteCarData : ScriptableObject {
    // Car stages
    public GameObject[] carStates = new GameObject[3];
    
    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

    [SerializeField] public int firstStageHitsNeeded, secondStageHitsNeeded;

    public void Initialize(CarTypeData carType, int starCount) {
        // Pass information from {CarTypeData} source
        carStates = (GameObject[]) carType.carStates.Clone();
        possibleAttackPointStage1 = (GameObject[]) carType.possibleAttackPointStage1.Clone();
        possibleAttackPointStage2 = (GameObject[]) carType.possibleAttackPointStage2.Clone();

        // TODO: Initialize {HitsNeeded} variables based on {starCount}
        firstStageHitsNeeded = 2;
        secondStageHitsNeeded = 3;
    }

    // Factory method for creating a {ConcreteCarData} object 
    public static ConcreteCarData CreateInstance(CarTypeData carType, int starCount) {
        ConcreteCarData data = CreateInstance<ConcreteCarData>();
        data.Initialize(carType, starCount);

        string fileName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/DynamicData/JobData/CarData.asset");
        AssetDatabase.CreateAsset(data, fileName);
        AssetDatabase.SaveAssets();

        return data;
    }
}