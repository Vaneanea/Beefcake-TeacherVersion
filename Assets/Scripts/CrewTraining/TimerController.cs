using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour {
    [Header("UI fields")]
    [SerializeField] private TMP_Text timerText;

    [Header("Gameplay values")]
    [SerializeField] private float totalSeconds = 10;

    private float timeRemaining;
    private bool timerRunning = true;

    private CrewTrainingManager trainManager;

    private void Start() {
        timeRemaining = totalSeconds;

        trainManager = GetComponent<CrewTrainingManager>();
    }

    private void Update() {
        if (timerRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            } else {
                timeRemaining = -1;
                timerRunning = false;
                DisplayTime();

                trainManager.OnTimerEnd();
            }
        }
    }

    private void DisplayTime() {
        float seconds = Mathf.FloorToInt((timeRemaining + 1) % 60);
        float milisec = Mathf.FloorToInt(((timeRemaining + 1) * 100) % 100);
        timerText.SetText(string.Format("{0:00}:{1:00}", seconds, milisec));
    }
}
