using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{

    public ConcreteCarData[] cars;
    public GameObject aCarHolder;
    private GameObject carHolder;

    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private CombatStatManager csm;


    void Awake()
    {
        SetGameManager();
        SetOtherManagers();
        CreateCarHolder();
    }

    void Update()
    {
        CheckIfCarIsDone();
    }

    private void CreateCarHolder()
    {
        GameObject x = Instantiate(aCarHolder);

        x.GetComponent<Car>().car = cars[Random.Range(0, cars.Length)];
        carHolder = x;
        
    }

    private void CheckIfCarIsDone()
    {
        if (carHolder.GetComponent<Car>().isDone == true)
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
        Destroy(carHolder);
        CreateCarHolder();
    }

    public GameObject GetCarHolder()
    {
        return carHolder;
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
