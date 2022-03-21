using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarData car;

    //The amount of attackpoint that have been destroyed ("fixed") during a stage
    [SerializeField] private float attackPointsDestroyed;
        
    [SerializeField]
    private int firstStageAttackPointAmount, secondStageAttackPointAmount;

    
    [SerializeField]
    private int stagesDone;

    [SerializeField]
    private GameObject[] carStates;

    private GameObject attackPointPrefab;
    public Canvas canvas;

    private GameObject[] attackPointsStage1;
    private GameObject[] attackPointsStage2;

    private GameObject firstCarStage;
    private GameObject secondCarStage;
    private GameObject thirdCarStage;

    public bool isDone;
    public bool isWashed;

    private void Awake()
    {
        isDone = false;
    }

    void Start()
    {
        stagesDone = 0;

        //Assign scriptale object carstates to car
        carStates = car.carStates;

        //Assign how many hits are needed to progress
        firstStageAttackPointAmount = car.firstStageHitsNeeded;
        secondStageAttackPointAmount = car.secondStageHitsNeeded;

        //Assign a Prefab for what the attackpoint looks/functions like;
        attackPointPrefab = FindObjectOfType<GameManager>().transform.GetChild(1).GetComponent<CombatStatManager>().attackTargetPrefab;


        //Create an array of attackpoints that will e used during the existance of the car, give them the size required 
        attackPointsStage1 = new GameObject[firstStageAttackPointAmount];
        attackPointsStage2 = new GameObject[secondStageAttackPointAmount];




        //ask arjen aout duplicates

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


        

        //Instantiate  the car model in it's proper location        
        firstCarStage = Instantiate(carStates[0], transform);

        //Assigning each attacklocation an attack point prefab and spawn it with the correct transform information;
        foreach (GameObject attackPointLocation in attackPointsStage1) {

            GameObject attackPoint = attackPointPrefab;


            //Move Attackpoint to correct position on screen+
            attackPoint.GetComponent<RectTransform>().anchoredPosition = attackPointLocation.GetComponent<RectTransform>().anchoredPosition;

            
            GameObject x = Instantiate(attackPoint, canvas.transform);
            x.name = attackPointLocation.name;

            //put player on the right position
            x.transform.GetChild(0).transform.localPosition = attackPointLocation.transform.GetChild(0).transform.localPosition;
            x.transform.GetChild(0).transform.rotation = attackPointLocation.transform.GetChild(0).transform.rotation;

          

        }
    }

    void Update()
    {
       
        //fixing float reaches a number
        if (attackPointsDestroyed >= firstStageAttackPointAmount && stagesDone == 0)
        {

            Destroy(firstCarStage);

            secondCarStage = Instantiate(carStates[1], transform);
            foreach (GameObject attackPointLocation in attackPointsStage2)
            {

                GameObject attackPoint = attackPointPrefab;


                //Move Attackpoint to correct position on screen+
                attackPoint.GetComponent<RectTransform>().anchoredPosition = attackPointLocation.GetComponent<RectTransform>().anchoredPosition;

               
                GameObject x = Instantiate(attackPoint, canvas.transform);
                x.name = attackPointLocation.name;

                //put player on the right position
                x.transform.GetChild(0).transform.localPosition = attackPointLocation.transform.GetChild(0).transform.localPosition;
                x.transform.GetChild(0).transform.rotation = attackPointLocation.transform.GetChild(0).transform.rotation;

            }

            attackPointsDestroyed = 0;
            stagesDone = 1;


        }

        //fixing float reaches a number
        if (attackPointsDestroyed >= secondStageAttackPointAmount && stagesDone == 1)
        {

            Destroy(secondCarStage);

            thirdCarStage = Instantiate(carStates[2], transform);

            attackPointsDestroyed = 0;
            stagesDone = 2;

            StartCoroutine(WaitASec(0.5f));
        }
    }
    public void FixAttackPoint()
    {
        attackPointsDestroyed++;
    }

    public  IEnumerator WaitASec(float x)
    {
       yield return new WaitForSeconds(x);
        isDone = true;
    }
}

