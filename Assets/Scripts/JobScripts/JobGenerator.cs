using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generates a Job based on difficulty, which ranges from 1 - 100 (?)
public class JobGenerator : MonoBehaviour {
    // TODO: Difficulty can be calculated based on Stage and updated each time a new job is needed
    [SerializeField] private int difficulty;

    [SerializeField] private List<CarData> cars;

    public void GenerateJob() {
        // Generate number of Cars and their difficulty (which ranges from 1 to 5 stars)

        // Instantiate CarData objects and let them decide their stats based on their difficulty

        // Generate Rewards
    }

    // TODO: Handle Job Display and UI in the pop-up (probably make separate class)
}
