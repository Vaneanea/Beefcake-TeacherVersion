using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

// Concrete instance of a Job
// Holds information needed about the Job: Cars, ?Rewards?
public class JobData : ScriptableObject {

    [SerializeField] public List<DynamicCarData> cars;

    // TODO: Add reward fields

    public void Initialize(List<DynamicCarData> cars) {
        this.cars = cars;
    }

    // Factory method for creating a {JobData} object
    public static JobData CreateInstance(List<DynamicCarData> cars) {
        JobData data = CreateInstance<JobData>();
        data.Initialize(cars);

        //string fileName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/DynamicData/JobData/JobData.asset");
        //AssetDatabase.CreateAsset(data, fileName);
        //AssetDatabase.SaveAssets();

        return data;
    }
}
