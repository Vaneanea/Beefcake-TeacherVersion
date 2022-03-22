using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CombatStatManager : MonoBehaviour
{
    [Header("Attack Point Settings")]
    public int attackPointHitMax;
    public int staminaDecreaseValue = 1;
    public GameObject attackTargetPrefab;
    public GameObject progressBar;

    private int progressBarMaxValue;
    private int currentProgress = 0;

    //Managers
    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private CarManager cm;

   

    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
        SetInitalVariables();
    }


    private void SetInitalVariables()
    {
        SetInitialProgressBar();
    }

    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        cm = gm.GetCarManager();
    }

    private void SetInitialProgressBar()
    {
        int attackPointStage1 = cm.GetCarHolder().GetComponent<Car>().car.firstStageHitsNeeded;
        int attackPointStage2 = cm.GetCarHolder().GetComponent<Car>().car.secondStageHitsNeeded;

        progressBarMaxValue = attackPointStage1 + attackPointStage2;
        progressBar.GetComponent<SliderScript>().SetMaxHealth(attackPointHitMax * (progressBarMaxValue));
        progressBar.GetComponent<SliderScript>().SetHealth(0);
    }

    public void IncreaseProgress(int progress) 
    {
        currentProgress += progress;
        progressBar.GetComponent<SliderScript>().SetHealth(currentProgress);
    }

    public void ResetProgressBar() {

        currentProgress = 0;
        SetInitialProgressBar();
    }

}
