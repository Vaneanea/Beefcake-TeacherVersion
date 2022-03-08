using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarHolder : MonoBehaviour
{
    public Car car;

    //How many times the players has hit/tapped to attack
    [SerializeField] private float hits;

    //Max tap values
    [SerializeField]
    private int firstStageHitsNeeded, secondStageHitsNeeded;

    //stasge counter
    [SerializeField]
    private int stagesDone;

    [SerializeField]
    //car stages
    private GameObject[] carStates;


    private GameObject attackPointPrefab;

    private GameObject[] attackPointsStage1;
    private GameObject[] attackPointsStage2;

    private GameObject firstCarStage;
    private GameObject secondCarStage;
    private GameObject thirdCarStage;

    public bool isDone;
    public bool isWashed;




    void Start()
    {
        stagesDone = 0;

        //Assign scriptale object carstates to car
        carStates = car.carStates;

        //Assign how many hits are needed to progress
        firstStageHitsNeeded = car.firstStageHitsNeeded;
        secondStageHitsNeeded = car.firstStageHitsNeeded;

        //Assign a Prefab for what the attackpoint looks/functions like;
        attackPointPrefab = car.attackPointPrefab;


        //Create an array of attackpoints that will e used during the existance of the car, give them the size required 
        attackPointsStage1 = new GameObject[firstStageHitsNeeded];
        attackPointsStage2 = new GameObject[secondStageHitsNeeded];




        //Fill the array of attackpoints that will be used in stage 1 with a rondom assortment of possible attackponts
        for (int i = 0; i < attackPointsStage1.Length; i++)
        {
            int rn = Random.Range(0, car.possibleAttackPointStage1.Length);

            attackPointsStage1[i] = car.possibleAttackPointStage1[rn];

        }


        for (int i = 0; i < attackPointsStage2.Length; i++)
        {
            int rn = Random.Range(0, car.possibleAttackPointStage2.Length);

            attackPointsStage2[i] = car.possibleAttackPointStage1[rn];
        }


        isDone = false;

        //Instantiate  the car model in it's proper location        
        firstCarStage = Instantiate(carStates[0], transform);

        //Assigning each attacklocation an attack point prefab and spawn it with the correct transform information;
        foreach (GameObject attackPointLocation in attackPointsStage1) {

            GameObject attackPoint = attackPointPrefab;


            //if (firstCarStage.GetComponent<CarState>().hasLanded == true)
            //{
            //    GameObject x = Instantiate(attackPoint, transform);

            //    x.transform.position = attackPointLocation.transform.position;
            //    x.transform.rotation = attackPointLocation.transform.rotation;
            //    x.transform.localScale = new Vector3(1, 1, 1);
            //}

            GameObject x = Instantiate(attackPoint, transform);

            x.transform.position = attackPointLocation.transform.position;
            x.transform.rotation = attackPointLocation.transform.rotation;
            x.transform.localScale = new Vector3(1, 1, 1);

        }
    }


    void Update()
    {
        //Debug.Log(firstCarStage.GetComponent<CarState>().hasLanded + "hi");

        //fixing float reaches a number
        if (hits >= firstStageHitsNeeded && stagesDone == 0)
        {

            Destroy(firstCarStage);

            secondCarStage = Instantiate(carStates[1], transform);
            foreach (GameObject attackPointLocation in attackPointsStage2)
            {

                GameObject attackPoint = attackPointPrefab;

                //if (secondCarStage.GetComponent<CarState>().hasLanded == true)
                //{
                //    var x = Instantiate(attackPoint, transform);


                //    x.transform.position = attackPointLocation.transform.position;
                //    x.transform.rotation = attackPointLocation.transform.rotation;
                //    x.transform.localScale = new Vector3(1, 1, 1);
                //}

                var x = Instantiate(attackPoint, transform);


                x.transform.position = attackPointLocation.transform.position;
                x.transform.rotation = attackPointLocation.transform.rotation;
                x.transform.localScale = new Vector3(1, 1, 1);

            }

            hits = 0;
            stagesDone = 1;


        }

        //fixing float reaches a number
        if (hits >= secondStageHitsNeeded && stagesDone == 1)
        {

            Destroy(secondCarStage);

            thirdCarStage = Instantiate(carStates[2], transform);

            hits = 0;
            stagesDone = 2;

            StartCoroutine(WaitASec(1));
        }

    }

    public void FixItem()
    {
        hits++;
    }

    public  IEnumerator WaitASec(int x)
    {

        yield return new WaitForSeconds(x);
        isDone = true;
    }
}

