using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using UnityEditor;

// Holds a concrete instance of CarTypeData 
public class DynamicCarData : ScriptableObject {
    // Car stages
    public GameObject[] carStates = new GameObject[3];
    
    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

    public int firstStageHitsNeeded, secondStageHitsNeeded;

    //car difficulty 
    public int starCount;

    public void Initialize(CarTypeData carType, int starCount) {

        // Pass information from {CarTypeData} source
        carStates = (GameObject[]) carType.carStates.Clone();
        possibleAttackPointStage1 = (GameObject[]) carType.possibleAttackPointStage1.Clone();
        possibleAttackPointStage2 = (GameObject[]) carType.possibleAttackPointStage2.Clone();

        // TODO: Initialize {HitsNeeded} variables based on {starCount}
        firstStageHitsNeeded = 2;
        secondStageHitsNeeded = 3;

        this.starCount = starCount;
    }

    // Factory method for creating a {ConcreteCarData} object 
    public static DynamicCarData CreateInstance(CarTypeData carType, int starCount) {
        DynamicCarData data = CreateInstance<DynamicCarData>();
        data.Initialize(carType, starCount);

        string fileName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/DynamicData/JobData/CarData.asset");
        AssetDatabase.CreateAsset(data, fileName);
        AssetDatabase.SaveAssets();

        return data;
    }
}