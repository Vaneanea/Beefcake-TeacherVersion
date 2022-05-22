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
    private GameObject car;

    public Transform carDonePosition;

    private GameManager gm;
    private CombatStatManager csm;
    private JuiceManager jm;

    public int carDone = 0;
    [SerializeField]
    private int coinsEarned = 0;

    #region Job Game Mode variables
    public bool jobDone;
    private int carIndex;
    #endregion

    void Awake()
    {
        jobDone = false;
        carIndex = 0;

        carTypes = Resources.LoadAll<CarTypeData>("Data/CarData/StarLevel0");

        SetGameManager();
        SetOtherManagers();
        CreateCar();
    }

    void Update()
    {
        CheckIfCarIsDone();
    }

    // Called when player rejects job by interacting with the Client Info Pop-up
    public void OnCarRejected() {
        PrepareNextCar();
        jm.carSmokeEffects.Stop();
    }

    private void CreateCar()
    {
        GameObject newCar = Instantiate(aCar);

        if (gm.gameMode == GameMode.Endless)
            CreateRandomCar(ref newCar);
        if (gm.gameMode == GameMode.Job)
            CreateNextCar(ref newCar);

        car = newCar;
    }

    private void CreateRandomCar(ref GameObject newCar)
    {
        // Generate random CarTypeData
        var index = Random.Range(0, carTypes.Length);
        newCar.GetComponent<Car>().carTypeData = carTypes[index];

        // TODO: Generate random star count (hard-coded for now)
        DynamicCarData car = DynamicCarData.CreateInstance(carTypes[index]);
        newCar.GetComponent<Car>().dynamicCarData = car;
    }

    private void CreateNextCar(ref GameObject newCar)
    {
        List<DynamicCarData> cars = gm.job.cars;

        newCar.GetComponent<Car>().carTypeData = cars[carIndex].carType;
        newCar.GetComponent<Car>().dynamicCarData = cars[carIndex];

        carIndex++;
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

            PrepareNextCar();
        }
    }

    private void PrepareNextCar() {
        if (gm.gameMode == GameMode.Job && carDone >= gm.job.cars.Count) {
            jobDone = true;
            return;
        }

        ReplaceCar();
        csm.ResetProgressBar();
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