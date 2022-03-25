using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Generates a Job based on difficulty, which ranges from 0.0 - 1.0
// TODO: Figure how exactly we want to handle job generation
public class JobGenerator : MonoBehaviour {
    // TODO: Difficulty can be calculated based on Stage and updated each time a new job is needed
    [SerializeField] private float difficulty;

    [SerializeField] private List<ConcreteCarData> cars;

    [Header("Generation Fields")]
    [SerializeField] private int baseCarCount = 3;
    [SerializeField] private int baseStarCount = 2;

    private List<CarTypeData> carTypeSources;

    private void Start() {
        carTypeSources = Resources.LoadAll<CarTypeData>("Data/CarData").ToList();
    }

    public void GenerateJob() {
        int carCount = GenerateCarCount();
        for (int index = 1; index <= carCount; index++) {
            int starCount = GeneratStarCount();
            CarTypeData carType = GenerateCarType();

            // Instantiate CarData objects and let them decide their stats based on their difficulty
            ConcreteCarData car = ConcreteCarData.CreateInstance(carType, starCount);
            cars.Add(car);
        }

        // TODO: Generate Rewards
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

    // TODO: Handle Job Display and UI in the pop-up (probably make separate class)
}
