using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Car Data")]
    public CarTypeData[] carTypes;
    public DynamicCarData[] carDynamicData;
    public DynamicCarData currentCarDynamicData;
    public GameObject aCar;
    public GameObject car;

    [Header("Car Positions")]
    public Transform carStage1Position;
    public Transform carStage2Position;
    public Transform carDonePosition;

    private GameManager gm;
    private CombatStatManager csm;
    private JuiceManager jm;

    public int carDone = 0;
    [SerializeField]
    private int coinsEarned = 0;

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
        //TODO ALSO INSTANTIATE A dynamic car thinhie!!!!!!!
        var index = Random.Range(0, carTypes.Length);
        x.GetComponent<Car>().carTypeData = carTypes[index];
        x.GetComponent<Car>().dynamicCarData = carDynamicData[index];
        currentCarDynamicData = x.GetComponent<Car>().dynamicCarData;

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


            AddCoinsEarnedForCar();
            carDone++;
            ReplaceCar();
            csm.ResetProgressBar();
        }
    }

    //Is very simple right now, is open for expansion in the futore. Maybe 
    private void AddCoinsEarnedForCar()
    {
        var coinsEarned = car.GetComponent<Car>().dynamicCarData.starCount * 100;
        this.coinsEarned += coinsEarned;
        gm.coinsEarned = this.coinsEarned;
    }

    public int GetTotalCoinsEarned()
    {
        var carBonus = carDone * 25;

        coinsEarned += carBonus;

        return coinsEarned;
    }

    private void ReplaceCar()
    {
        Destroy(car.GetComponent<Car>().clientCard.gameObject);
        Destroy(car);

        CreateCar();
        jm.SetCurrentActiveSmokePillars();

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
        jm = gm.GetJuiceManager();
    }

}