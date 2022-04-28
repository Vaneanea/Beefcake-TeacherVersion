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

    [Header("Private Managers")]
    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private CarManager cm;

    
    public List<GameObject> currentAttackPoints;



    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
        SetInitalVariables();
    }


    private void SetInitalVariables()
    {
        SetInitialProgressBar();
        currentAttackPoints = new List<GameObject>();
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
        int attackPointStage1 = cm.GetCar().GetComponent<Car>().dynamicCarData.firstStageHitsNeeded;
        int attackPointStage2 = cm.GetCar().GetComponent<Car>().dynamicCarData.secondStageHitsNeeded;

        progressBarMaxValue = attackPointStage1 + attackPointStage2;
        progressBar.GetComponent<SliderScript>().SetMaxValue(attackPointHitMax * (progressBarMaxValue));
        progressBar.GetComponent<SliderScript>().SetValue(0);
    }

    public void IncreaseProgress(int progress) 
    {
        currentProgress += progress;
        progressBar.GetComponent<SliderScript>().SetValue(currentProgress);
    }

    public void ResetProgressBar() {

        currentProgress = 0;
        SetInitialProgressBar();
    }

    

}
