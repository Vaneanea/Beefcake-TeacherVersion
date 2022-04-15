using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewTrainingManager : MonoBehaviour {
    [SerializeField] private CrewBeefCake curBeefCake;

    private TrainingUIManager uiManager;

    private void Start() {
        SetManagers();
        SetCurBeefCake();

        uiManager.OnStart(curBeefCake);
    }

    private void OnEnable() {
        TrainButtonController.OnTrain += IncreaseStat;
    }

    private void OnDisable() {
        TrainButtonController.OnTrain -= IncreaseStat;
    }

    private void IncreaseStat(int amount) {
        curBeefCake.strength += amount;

        uiManager.NotifyStatTrained(curBeefCake);
    }

    private void SetManagers() {
        uiManager = GetComponentInChildren<TrainingUIManager>();
    }

    private void SetCurBeefCake() {
        // TODO: Implement this! Pass information from other scenes.
        // Right now, {curBeefCake} is set manually through the Editor
    }
}
