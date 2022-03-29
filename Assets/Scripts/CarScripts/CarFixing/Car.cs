using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car Data Source")]
    public CarData car;

    [Header("Private AttackPoint Variables")]
    [SerializeField] private float attackPointsFixed;

    [SerializeField]
    private int firstStageAttackPointAmount, secondStageAttackPointAmount;
    
    private GameObject attackPointPrefab;
    private GameObject[] attackPointsStage1;
    private GameObject[] attackPointsStage2;

    [Header("Fix Stage")]
    [SerializeField]
    private int stagesDone;

    [Header("Car States")]
    [SerializeField]
    private GameObject[] carStates;

   //Managers
    private GameManager gm;
    private CombatStatManager csm;
    private JuiceManager jm;

    //Car Stages
    public GameObject firstCarStage;
    private GameObject secondCarStage;
    private GameObject thirdCarStage;
    public GameObject currentCarStage;
    

    [Header("Booleans")]
    public bool isDone;
    public bool isWashed;
    public bool hasLanded = false;

    void Start()
    {
        isDone = false;
        stagesDone = 0;

        SetInitialValues();

        //Instantiate  the car model in it's proper location        
        firstCarStage = Instantiate(carStates[0], transform);
        currentCarStage = firstCarStage;


        CreateAttackPoints(attackPointsStage1);

    }

    void Update()
    {

        //check if first stage has been fixed
        if (attackPointsFixed >= firstStageAttackPointAmount && stagesDone == 0)
        {
            Destroy(firstCarStage);
            secondCarStage = Instantiate(carStates[1], transform);
            currentCarStage = firstCarStage;
            jm.carSmokeEffects.Stop();
            CreateAttackPoints(attackPointsStage2);
            attackPointsFixed = 0;
            stagesDone = 1;
        }

        //check if second stage has been fixed
        if (attackPointsFixed >= secondStageAttackPointAmount && stagesDone == 1)
        {
            Destroy(secondCarStage);
            thirdCarStage = Instantiate(carStates[2], transform);
            currentCarStage = thirdCarStage;
            attackPointsFixed = 0;
            stagesDone = 2;
            StartCoroutine(MarkAsDone(0.5f));
        }
    }

    #region Set Managers
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        csm = gm.GetCombatStatManager();
        jm = gm.GetJuiceManager();
    }
    #endregion

    #region Set Inital Values Values
    private void SetInitialValues()
    {
        SetGameManager();

        SetOtherManagers();

        SetCarStates();

        SetAmountAttackPointsPerStage();

        SetAttackPointVisual();

        SetAttackPointsPerStage(attackPointsStage1, car.possibleAttackPointStage1);
        SetAttackPointsPerStage(attackPointsStage2, car.possibleAttackPointStage2);

        //Test(attackPointsStage1, car.possibleAttackPointStage1);
        //Test(attackPointsStage2, car.possibleAttackPointStage2);
    }

    private void SetCarStates()
    {
        carStates = car.carStates;
    }

    private void SetAmountAttackPointsPerStage()
    {
        //Assign how many hits are needed to progress
        firstStageAttackPointAmount = car.firstStageHitsNeeded;
        secondStageAttackPointAmount = car.secondStageHitsNeeded;

        //Create an array of attackpoints that will be used during the existance of the car, give them the size required 
        attackPointsStage1 = new GameObject[firstStageAttackPointAmount];
        attackPointsStage2 = new GameObject[secondStageAttackPointAmount];
    }
    private void SetAttackPointVisual()
    {

        attackPointPrefab = csm.attackTargetPrefab;
    }

    //ask arjen aout duplicates
    private void SetAttackPointsPerStage(GameObject[] attackPointCollectionForStage, GameObject[] possibleAttackPointsForStage)
    {

        //Fill the array of attackpoints that will be used in the stage with a rondom assortment of possible attackponts
        for (int i = 0; i < attackPointCollectionForStage.Length; i++)
        {
            int rn = Random.Range(0, possibleAttackPointsForStage.Length);

            attackPointCollectionForStage[i] = possibleAttackPointsForStage[rn];
        }
    }
    #endregion

    private void CreateAttackPoints(GameObject[] attackPointCollectionForStage)
    {
        //Assigning each attacklocation an attack point prefab and spawn it with the correct transform information;
        foreach (GameObject attackPointLocation in attackPointCollectionForStage)
        {
            GameObject attackPoint = attackPointPrefab;

            //Move Attackpoint to correct position on screen+
            attackPoint.GetComponent<RectTransform>().anchoredPosition = attackPointLocation.GetComponent<RectTransform>().anchoredPosition;

            GameObject x = Instantiate(attackPoint, gm.canvas.transform);
            x.name = attackPointLocation.name;

            
            //put player on the right position
            x.transform.GetChild(0).transform.localPosition = attackPointLocation.transform.GetChild(0).transform.localPosition;
            x.transform.GetChild(0).transform.rotation = attackPointLocation.transform.GetChild(0).transform.rotation;
        }
    }

    public void FixAttackPoint()
    {
        attackPointsFixed++;

        if (jm.currentlyActiveSmokePillars > 0)
        {
            //Remove Smoke partially by stopping one of the pillars
            jm.carSmokeEffects.gameObject.transform.GetChild(jm.currentlyActiveSmokePillars - 1).GetComponentInChildren<ParticleSystem>().Stop();

            //lower the active pillar number so the next pillar in line is turned off
            jm.currentlyActiveSmokePillars --;

        }
    
    }

    public IEnumerator MarkAsDone(float x)
    {
        yield return new WaitForSeconds(x);
        SetHasLanded(false);
        isDone = true;
    }

    public void SetHasLanded(bool hasLanded)
    {
       this.hasLanded = hasLanded;
    }

    private void Test(GameObject[] attackPointCollectionForStage, GameObject[] possibleAttackPointsForStage)
    {
       

        //Fill the array of attackpoints that will be used in the stage with a random assortment of possible attackponts
        for (int i = 0; i < attackPointCollectionForStage.Length; i++)
        {
            IEnumerable<GameObject> list = attackPointCollectionForStage.ToList<GameObject>();

            if (TryGetAttackPoint(list, possibleAttackPointsForStage, out var attackPoint) == true)
            {
                //Debug.Log(attackPoint);
                attackPointCollectionForStage[i] = attackPoint;

            }

            //int rn = Random.Range(0, possibleAttackPointsForStage.Length);

            //attackPointCollectionForStage[i] = possibleAttackPointsForStage[rn];
        }
    }
    internal bool TryGetAttackPoint(IEnumerable<GameObject> list, GameObject[] possibleAttackPointsForStage, out GameObject attackPoint)
    {
        attackPoint = null;
        List<GameObject> possibleAttackPoints = possibleAttackPointsForStage.ToList<GameObject>();

        if (list.Count() >= possibleAttackPoints.Count)
        {
            return false;
        }

        //bool contains = false;
        int index = Random.Range(0, possibleAttackPoints.Count);
        bool contains = list.Contains(possibleAttackPoints[index]);

        while (contains == true)
        {
            
            index = Random.Range(0, possibleAttackPoints.Count);
            contains = list.Contains(possibleAttackPoints[index]);
            
        }

        Debug.Log(possibleAttackPoints[index].name);
        attackPoint = possibleAttackPoints[index];
        return true;
    }

}

