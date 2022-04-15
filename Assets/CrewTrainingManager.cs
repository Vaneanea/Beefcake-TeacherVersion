using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewTrainingManager : MonoBehaviour {
    [SerializeField] private CrewBeefCake curBeefCake;

    private TrainingUIManager uiManager;

    void Start() {
        SetManagers();
        SetCurBeefCake();

        uiManager.OnStart(curBeefCake);
    }

    private void SetManagers() {
        uiManager = GetComponentInChildren<TrainingUIManager>();
    }

    private void SetCurBeefCake() {
        // TODO: Implement this! Pass information from other scenes.
        // Right now, {curBeefCake} is set manually through the Editor
    }
}
