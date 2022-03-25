using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceManager : MonoBehaviour
{

    [Header("Settings Car Shake")]
    [Range(0f, 2f)]
    public float time = 0.2f;
    [Range(0f, 2f)]
    public float distance = 0.1f;
    [Range(0f, 0.1f)]
    public float delayBetweenShakes = 0f;

    [Header("Private Managers")]
    private GameManager gm;
    private CarManager cm;
    private CarMainBody car;


    void Start()
    {
        SetGameManager();
        SetOtherManagers();
    }

    // Update is called once per frame
    void Update()
    {
        car = cm.GetCar().gameObject.GetComponentInChildren<CarMainBody>();
    }


    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        cm = gm.GetCarManager();
    }


    public void ShakeCar()
    {
        StartCoroutine(car.Shake());
    }


 

}
