using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;
using UnityEditor;
using Random = UnityEngine.Random;
using System.Linq;

// Holds a concrete instance of CarTypeData 
public class DynamicCarData : ScriptableObject
{
    private const int maxFirstStageHits = 2;
    private const int maxSecondStageHits = 3;
    private const int maxStarCount = 3;

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
    public void Initialize(CarTypeData carType)
    {
        this.carType = carType;

        // Pass information from {CarTypeData} source
        carStates = (GameObject[])carType.carStates.Clone();
        possibleAttackPointStage1 = (GameObject[])carType.possibleAttackPointStage1.Clone();
        possibleAttackPointStage2 = (GameObject[])carType.possibleAttackPointStage2.Clone();

        starCount = GenerateStarCount();

        GenerateHitsNeeded();
       
        needFix = DecideIfFixIsNeeded();
        needWash = DecideIfWashIsNeeded();

        //Store all Gameobjects in an array like this
        var allClients = Resources.LoadAll<GameObject>("Clients/ClientSpritePrefabs");

        //You can use ToList() function as you are using Linq
        var clientList = allClients.ToList();

        clientVisuals = clientList[Random.Range(0, clientList.Count)];
    }

    // Factory method for creating a {ConcreteCarData} object 
    public static DynamicCarData CreateInstance(CarTypeData carType)
    {
        DynamicCarData data = CreateInstance<DynamicCarData>();
        data.Initialize(carType);

        return data;
    }

    #region Car Generation methods
    private int GenerateStarCount() {
        return Random.Range(1, 4);
    }

    // Set {firstStageHitsNeeded} and {secondStageHitsNeeded} based on {starCount}
    // -- This method could use some work in the future
    private void GenerateHitsNeeded() {
        
        switch (starCount) {
            case 1:
                firstStageHitsNeeded = secondStageHitsNeeded = 1;
                break;
            case 2:
                firstStageHitsNeeded = Random.Range(1, maxFirstStageHits + 1) + Random.Range(0, starCount);
                secondStageHitsNeeded = Random.Range(1, maxSecondStageHits + 1) + Random.Range(0, starCount);

                firstStageHitsNeeded = Math.Min(firstStageHitsNeeded, maxFirstStageHits);
                secondStageHitsNeeded = Math.Min(secondStageHitsNeeded, maxSecondStageHits);

                break;
            case 3:
                firstStageHitsNeeded = maxFirstStageHits;
                secondStageHitsNeeded = maxSecondStageHits;
                break;
        }
    }
    
    private bool DecideIfWashIsNeeded()
    {
        var x = Random.Range(0, 1);
        if (x == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool DecideIfFixIsNeeded()
    {
        var x = Random.Range(0, 1);
        if (x == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}