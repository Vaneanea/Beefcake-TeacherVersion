using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Generates a Job based on difficulty, which ranges from 0.0 - 1.0
// TODO: Figure how exactly we want to handle job generation
public class JobGenerator : MonoBehaviour {
    // TODO: Difficulty can be calculated based on Stage and updated each time a new job is needed
    [SerializeField] private float difficulty;

    [SerializeField] private List<DynamicCarData> cars;

    [Header("Generation Fields")]
    [SerializeField] private int baseCarCount = 3;
    [SerializeField] private int baseStarCount = 2;

    private List<CarTypeData> carTypeSources;

    private JobUIManager uiManager;

    private void Start() {
        carTypeSources = Resources.LoadAll<CarTypeData>("Data/CarData").ToList();
        uiManager = GetComponent<JobUIManager>();
       // ClearOldJob();
    }

    public void GenerateRandomJob() { 
        //ClearOldJob();

        int carCount = GenerateCarCount();

        for (int index = 1; index <= carCount; index++) {
            int starCount = GeneratStarCount();
            CarTypeData carType = GenerateCarType();

            uiManager.AddCarSlot(carType, starCount);

            var needWash = GenerateNeedWashing();
            var needFix = GenerateNeedFixing();
            // Instantiate CarData objects and let them decide their stats based on their difficulty
            DynamicCarData car = DynamicCarData.CreateInstance(carType, starCount, needWash, needFix);
            cars.Add(car);
        }

        // TODO: Generate Rewards

        // Create the JobData object that incorporates all the previously generated information
        JobData.CreateInstance(cars);
    }

    /// This is commented out ecause the saving system still needs to e implimented here

    //public void ClearOldJob()
    //{
    //    cars.Clear();

    //    uiManager.ClearOldJobUI();

    //    // Remove old ConcreteCarData assets
    //    string[] jobFolder = { "Assets/Resources/DynamicData/JobData" };
    //    foreach (var asset in AssetDatabase.FindAssets("", jobFolder))
    //    {
    //        var path = AssetDatabase.GUIDToAssetPath(asset);
    //        AssetDatabase.DeleteAsset(path);
    //    }
    //}

    #region Generation Methods
    private bool GenerateNeedWashing()
    {
        float rand = Random.value;
        if (rand % 2 == 0)
        {
           return true;
        }
        return false;
    }

    private bool GenerateNeedFixing()
    {
        float rand = Random.value;
        if (rand % 2 == 0)
        {
            return true;
        }
        return false;
    }

    // Generates number of cars based on {difficulty}
    private int GenerateCarCount() {
        float rand = Random.value;
        if (rand < difficulty)
            return baseCarCount + 1;
        return baseCarCount;
    }

    // Generate number difficulty of a car (which ranges from 1 to 5 stars)
    private int GeneratStarCount() {
        // TODO: !!! Make this generation better !!!
        float rand = Random.value;
        if (rand < difficulty)
            return baseStarCount + 1;
        return baseStarCount;
    }

    // Generate CarType to be used
    private CarTypeData GenerateCarType() {
        // TODO: !!! Make this generation better !!! For now it's random
        int index = Random.Range(0, carTypeSources.Count);
        return carTypeSources[index];
    }
    #endregion

    // TODO: Handle Job Display and UI in the pop-up (probably make separate class)
}
