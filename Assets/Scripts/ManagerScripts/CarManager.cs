using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Car Data")]
    public CarData[] cars;
    public GameObject aCar;
    private GameObject car;

    [Header("Private Managers")]
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private CombatStatManager csm;


    void Awake()
    {
        SetGameManager();
        SetOtherManagers();
        CreateCar();
    }

    void Update()
    {
        CheckIfCarIsDone();
    }

    private void CreateCar()
    {
        GameObject x = Instantiate(aCar);

        x.GetComponent<Car>().car = cars[Random.Range(0, cars.Length)];
        car = x;
        
    }

    private void CheckIfCarIsDone()
    {
        if (car.GetComponent<Car>().isDone == true)
        {
            ///activate when washing is implemented
            ////for carwash, move camera to correct position//
            //GetComponentInParent<GameManager>().cam.GetComponent<CameraBehaviour>().isWashing = true;

            //if (carHolder.GetComponent<CarHolder>().isWashed == true)
            //{
            //    StartCoroutine(WaitASec(1));
            //}

            ReplaceCar();
            csm.ResetProgressBar();
        }
    }

    private void ReplaceCar() {
        Destroy(car);

        CreateCar();
    }


    public GameObject GetCar()
    {
        return car;
    }



    //Managers
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        csm = gm.GetCombatStatManager();
    }

}
