using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using UnityEditor;
using Random = UnityEngine.Random;
using System.Linq;

// Holds a concrete instance of CarTypeData 
public class DynamicCarData : ScriptableObject {
    // Car stages
    public GameObject[] carStates = new GameObject[3];
    
    public GameObject[] possibleAttackPointStage1;
    public GameObject[] possibleAttackPointStage2;

    public int firstStageHitsNeeded, secondStageHitsNeeded;

    //car difficulty 
    public int starCount;

    //Make the allocation of these random in the future
    public bool needWash;
    public bool needFix;
    public GameObject clientVisuals;

    public CarTypeData carType;
    public void Initialize(CarTypeData carType, int starCount) {
        this.carType = carType;

        // Pass information from {CarTypeData} source
        carStates = (GameObject[]) carType.carStates.Clone();
        possibleAttackPointStage1 = (GameObject[]) carType.possibleAttackPointStage1.Clone();
        possibleAttackPointStage2 = (GameObject[]) carType.possibleAttackPointStage2.Clone();

        // TODO: Initialize {HitsNeeded} variables based on {starCount}
        firstStageHitsNeeded = 2;
        secondStageHitsNeeded = 3;

        this.starCount = starCount;
        needFix = DecideIfFixIsNeeded();
        needWash = DecideIfWashIsNeeded();

        //Store all Gameobjects in an array like this
        var allClients = Resources.LoadAll<GameObject>("Clients/ClientSpritePrefabs");

        //You can use ToList() function as you are using Linq
        var clientList = allClients.ToList();

        clientVisuals = clientList[Random.Range(0, clientList.Count)];
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

    #region Car Generation methods
    private bool DecideIfWashIsNeeded() {
        var x = Random.Range(0, 1);
        if (x == 1) {
            return true;
        } else {
            return false;
        }
    }

    private bool DecideIfFixIsNeeded() {
        var x = Random.Range(0, 1);
        if (x == 1) {
            return true;
        } else {
            return false;
        }
    }
    #endregion
}