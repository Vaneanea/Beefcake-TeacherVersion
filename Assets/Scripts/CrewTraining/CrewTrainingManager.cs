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
        StartCoroutine(EndTrainingAfterSeconds(trainingTime));
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
        saveManager = GetComponentInChildren<SaveManager>();
    }

    private IEnumerator EndTrainingAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);

        Debug.Log("END TRAINING SESSION");

        SaveBeefCakeChanges();
        // TODO: Show pop-up with progress or something before switching scenes 
        SceneManager.LoadScene("CrewManagement");
    }

    private void SaveBeefCakeChanges() {
        List<CrewBeefCake> crew = SaveManager.crew;
        foreach (CrewBeefCake beef in crew) {
            if (beef.displayName == curBeefCake.displayName)
                beef.strength = curBeefCake.strength;
        }

        saveManager.Save();
    }
}
