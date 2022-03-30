//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using Random = UnityEngine.Random;

//public class TestJobGenerator : MonoBehaviour
//{
//    public List<TestJob> currentJobs;

//    private List<CarTypeData> carTypeSource;

//    private JobUIManager uiManager;

//    private GameManager gm;


//    ////TODO Need to find andother way of generating so might not e used in the feature
//    //[Header("Generation Fields")]
//    //[SerializeField] private int baseCarCount = 3;
//    //[SerializeField] private int baseStarCount = 2;

//    //private List<CarTypeData> carTypeSources;


//    private void Start()
//    {
//        SetGameManager();

//        //temporary fix, job generator takes cars from the reputation star level that the player currently is.
//        var currentCrewData = gm.GetCrewCurrentReputation();
//        carTypeSource = Resources.LoadAll<CarTypeData>("Data/CarData/StarLevel" + currentCrewData.ToString()).ToList();
//        uiManager = GetComponent<JobUIManager>();
//    }

//    void Update()
//    {
        
//    }
//    private void SetGameManager()
//    {
//        gm = FindObjectOfType<GameManager>();
//    }
//    public void GenerateRandomJobs()
//    {
//        ClearOldJobs();



//        int carCount = GenerateCarCount();
//        for (int index = 1; index <= carCount; index++)
//        {
//            int starCount = GeneratStarCount();
//            CarTypeData carType = GenerateCarType();

//            uiManager.AddCarSlot(carType, starCount);

//            // Instantiate CarData objects and let them decide their stats based on their difficulty
//            DynamicCarData car = DynamicCarData.CreateInstance(carType, starCount);
//            cars.Add(car);
//        }

//        //TODO: Generate Rewards
//    }

   

//    private void ClearOldJobs()
//    {
//        throw new NotImplementedException();
//    }

//    #region Generation Methods

//    // Generates number of cars based on {difficulty}
//    private int GenerateCarCount()
//    {
//        return Random.Range(3, );
//    }

//    // Generate number difficulty of a car (which ranges from 1 to 5 stars)
//    private int GeneratStarCount()
//    {
//        // TODO: !!! Make this generation better !!!
//        float rand = Random.value;
//        if (rand < difficulty)
//            return baseStarCount + 1;
//        return baseStarCount;
//    }

//    // Generate CarType to be used
//    private CarTypeData GenerateCarType()
//    {
//        // TODO: !!! Make this generation better !!! For now it's random
//        int index = Random.Range(0, carTypeSources.Count);
//        return carTypeSources[index];
//    }
//    #endregion
//}
