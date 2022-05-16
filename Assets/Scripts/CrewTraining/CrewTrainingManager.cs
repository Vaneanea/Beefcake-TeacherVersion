using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrewTrainingManager : MonoBehaviour {
    [SerializeField] public static CrewBeefCake curBeefCake;
    [SerializeField] public static string trainStat;

    [SerializeField] private int trainingTime;

    private SaveManager saveManager;
    private TrainingUIManager uiManager;

    private void Start() {
        if (SceneManager.GetActiveScene().name != "CrewTraining")
            return;

        SetManagers();

        uiManager.OnStart(curBeefCake);
    }

    private void OnEnable() {
        TrainButtonController.OnTrain += IncreaseStat;
    }

    private void OnDisable() {
        TrainButtonController.OnTrain -= IncreaseStat;
    }

    public void OnTimerEnd() {
        // TODO: Show pop-up with progress or something before switching scenes 
        SaveBeefCakeChanges();
        SceneManager.LoadScene("CrewManagement");
    }

    private void IncreaseStat(int amount) {
        switch (trainStat) {
            case "Strength":
                curBeefCake.strength += amount;
                break;
            case "Speed":
                curBeefCake.speed += amount;
                break;
            case "Stamina":
                curBeefCake.stamina += amount;
                break;
        }

        uiManager.NotifyStatTrained(curBeefCake);
    }

    private void SetManagers() {
        uiManager = GetComponentInChildren<TrainingUIManager>();
        saveManager = GetComponentInChildren<SaveManager>();
    }

    private void SaveBeefCakeChanges() {
        List<CrewBeefCake> crew = SaveManager.crew;
        foreach (CrewBeefCake beef in crew) {
            if (beef.displayName == curBeefCake.displayName) {
                beef.strength = curBeefCake.strength;
                beef.stamina = curBeefCake.stamina;
                beef.speed = curBeefCake.speed;
            }
        }

        saveManager.Save();
    }
}
