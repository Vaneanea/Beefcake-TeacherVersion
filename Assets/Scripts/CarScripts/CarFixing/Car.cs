using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Car : MonoBehaviour
{
    [Header("Car Data Source")]
    public CarTypeData carTypeData;
    public DynamicCarData dynamicCarData;

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
    private CarManager cm;
    private CombatStatManager csm;
    private JuiceManager jm;
    private CrewBeefCakeManager bcm;

    //Car Stages
    public GameObject firstCarStage;
    private GameObject secondCarStage;
    private GameObject thirdCarStage;
    public GameObject currentCarStage;


    [Header("Booleans")]
    public bool isDone;
    public bool isWashed;
    public bool hasLanded = false;

    private bool startPosIsAssigned = false;

    public GameObject clientCard;
    private List<Image> clientCardImages;
    private float fadeSpeed = 1f;

  /// <summary>
  /// TODO: ADD method so that attackpoints spawn after the car has landed
  /// </summary>
    void Start()
    {
        isDone = false;
        stagesDone = 0;

        SetInitialValues();

        //Instantiate  the car model in it's proper location        
        firstCarStage = Instantiate(carStates[0]);
        firstCarStage.transform.SetParent(cm.GetCar().transform);
        currentCarStage = firstCarStage;

        CreateAttackPoints(attackPointsStage1);

        CreateClientCardVisual();
        clientCard.SetActive(false);
        SetClientCardImages();
    }

    void Update()
    {
        ActivateClientCard();
      
        FadeOutClientCard();

        //check if first stage has been fixed
        if (attackPointsFixed >= firstStageAttackPointAmount && stagesDone == 0)
        {
            Destroy(firstCarStage);
            jm.carSmokeEffects.Stop();
            secondCarStage = Instantiate(carStates[1]);
            secondCarStage.transform.SetParent(cm.GetCar().transform);
            StartCoroutine(jm.Cheer());

            currentCarStage = secondCarStage;
            
            CreateAttackPoints(attackPointsStage2);
            attackPointsFixed = 0;
            stagesDone = 1;
        }

        //check if second stage has been fixed
        if (attackPointsFixed >= secondStageAttackPointAmount && stagesDone == 1)
        {
            Destroy(secondCarStage);
            thirdCarStage = Instantiate(carStates[2]);
            thirdCarStage.transform.SetParent(cm.GetCar().transform);
           

            currentCarStage = thirdCarStage;
            attackPointsFixed = 0;
            stagesDone = 2;
            StartCoroutine(MarkAsDone(0.5f));
        }
    }


    private void ActivateClientCard()
    {
        if (hasLanded == true && startPosIsAssigned == false)
        {
            clientCard.SetActive(true);
            GetComponentInChildren<CarMainBody>().AssignStartPosition();
            startPosIsAssigned = true;
        }

    }

    private void FadeOutClientCard()
    {
        //fadout over time
        if (clientCard.activeSelf == true)
        {
            StartCoroutine(Fade());
        }
    }




    private void CreateClientCardVisual()
    {
        //card creation and icon 
        var clientCard = Resources.Load<GameObject>("Clients/ClientCardPrefabs/ClientInfo");
        var x = Instantiate(clientCard, gm.canvas.transform.GetChild(3).GetChild(0).transform);
        x.transform.GetChild(0).GetComponent<Image>().sprite = dynamicCarData.clientVisuals.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

        //adding stars
        for (int i = 0; i < dynamicCarData.starCount; i++)
        {
            var star = Resources.Load<GameObject>("Clients/ClientCardPrefabs/StarIcon");
            Instantiate(star, x.transform.GetChild(1).transform);
        }

        //Adding order requirements
        if (dynamicCarData.needWash == true)
        {
            x.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        }
        if (dynamicCarData.needFix == true)
        {
            x.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        }

        this.clientCard = x;
    }



    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);

        foreach (Image img in clientCardImages)
        {
            Color color = clientCard.GetComponent<Image>().color;
            float fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            img.color = new Color(color.r, color.g, color.b, fadeAmount);
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
        cm = gm.GetCarManager();
        bcm = gm.GetBeefcakeManager();
        
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

        attackPointsStage1 = SetAttackPointsPerStage(firstStageAttackPointAmount, carTypeData.possibleAttackPointStage1);
        attackPointsStage2 = SetAttackPointsPerStage(secondStageAttackPointAmount, carTypeData.possibleAttackPointStage2);

    }

    private void SetCarStates()
    {
        carStates = carTypeData.carStates;
    }

    private void SetAmountAttackPointsPerStage()
    {
        //Assign how many hits are needed to progress
        firstStageAttackPointAmount = dynamicCarData.firstStageHitsNeeded;
        secondStageAttackPointAmount = dynamicCarData.secondStageHitsNeeded;

    }
    private void SetAttackPointVisual()
    {

        attackPointPrefab = csm.attackTargetPrefab;
    }

    
    private GameObject[] SetAttackPointsPerStage(int amountOfAttackPointsNeeded, GameObject[] possibleAttackPointsForStage)
    {
        List<GameObject> list = possibleAttackPointsForStage.ToList();
        var amount = amountOfAttackPointsNeeded;
       

        List<GameObject> alreadyUsed = new List<GameObject>();
        List<GameObject> randomList = new List<GameObject>();


        for (int i = 0; i < amount;)
        {
            int rn = Random.Range(0, amount);

            if (!alreadyUsed.Contains(list[rn]))
            {
                randomList.Add(list[rn]);
                alreadyUsed.Add(list[rn]);
                i++;
            }
        }

       return randomList.ToArray();

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

    private void SetClientCardImages()
    {
        Image[] clientCardImages = clientCard.GetComponentsInChildren<Image>();
        var clientCardImagesList = clientCardImages.ToList();

        clientCardImagesList.Add(clientCard.GetComponent<Image>());

        this.clientCardImages = clientCardImagesList;
    }

    public void FixAttackPoint()
    {
        attackPointsFixed++;

        if (jm.currentlyActiveSmokePillars > 0)
        {
            //Remove Smoke partially by stopping one of the pillars
            jm.carSmokeEffects.gameObject.transform.GetChild(jm.currentlyActiveSmokePillars - 1).GetComponentInChildren<ParticleSystem>().Stop();
            jm.carSmokeEffects.gameObject.transform.GetChild(jm.currentlyActiveSmokePillars - 2).GetComponentInChildren<ParticleSystem>().Stop();
            

            //lower the active pillar number so the next pillar in line is turned off
            jm.currentlyActiveSmokePillars -= 2;

        }

    }

    public IEnumerator MarkAsDone(float x)
    {
        yield return new WaitForSeconds(x);

        if (bcm.playerBeefcake.GetComponent<BeefCake>().beefCake.isFatigued == false)
        {
            float timeElapsed = 0;
            Vector3 startPos = currentCarStage.transform.position;

            Vector3 targetPos = cm.carDonePosition.transform.position;
            SoundEffectManager.Play("CarDrivingAway");
            while (timeElapsed < 1f)
            {
                thirdCarStage.transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / 1f);
                timeElapsed += Time.deltaTime;
                yield return null;
            }


            thirdCarStage.transform.position = targetPos;
        }
     



        SetHasLanded(false);
        isDone = true;
    }

    public void SetHasLanded(bool hasLanded)
    {
        this.hasLanded = hasLanded;
    }

}

