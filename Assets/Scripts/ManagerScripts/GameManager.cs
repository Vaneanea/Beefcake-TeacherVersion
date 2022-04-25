using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameMode { Job, Endless }

[SerializeField]
public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public Camera cam;
    public CrewInventory crewInventory;
    public GameObject musicManager;

    private CrewBeefCakeManager bcm;
    private CombatStatManager csm;
    private AttackAnimationManager aam;
    private CarManager cm;
    private SoundEffectManager sem;
    private JuiceManager jm;
    private CoinManager coinM;


    private int currentCrewReputation;

    private bool hasDeactivated = false;

    public int coinsEarned;
    private bool earningsHaveBeenCalculated = false;

    // {gameMode} - only used in the FixLoop
    public GameMode gameMode;
    public JobData job;

    [Header("Rewards")]
    public GameObject  coinReward;
    public GameObject gemReward;
    

    private void Awake()
    {
        SetInitialVariables();
    }


    void Update()
    {
        if (gameObject.name == "FixLoop_GameManager")
            FixLoopUpdate();
    }

    private void FixLoopUpdate() {
        if (gameMode == GameMode.Job) {
            if (cm.jobDone && !earningsHaveBeenCalculated)
                OnFixLoopEnd();
        }

        if (gameMode == GameMode.Endless) {

            if (bcm.playerBeefcake.GetComponent<BeefCake>().beefCake.isFatigued == true && earningsHaveBeenCalculated == false) {
                OnFixLoopEnd();
            }
        }

        if (canvas.gameObject.transform.GetChild(2).gameObject.activeInHierarchy == true) {
            DeActivateAttackPoints();
            hasDeactivated = true;
        }
        
        if (canvas.gameObject.transform.GetChild(2).gameObject.activeInHierarchy == false && canvas.gameObject.transform.GetChild(1).gameObject.activeInHierarchy == false && hasDeactivated == true) {
            ActivateAttackPoints();
            hasDeactivated = false;

        }
    }

    private void OnFixLoopEnd()
    {
        var fatigueScreen = canvas.gameObject.transform.GetChild(1);
        coinM.GetTotalCoinsEarned();
        fatigueScreen.gameObject.SetActive(true);
        coinReward.GetComponent<TextMeshProUGUI>().text = coinM.GetTotalCoinsEarned().ToString();
        var specialGems = cm.carDone / 4;
        gemReward.GetComponent<TextMeshProUGUI>().text = Mathf.Round(specialGems).ToString();
        earningsHaveBeenCalculated = true;

        if (canvas.gameObject.transform.GetChild(1).gameObject.activeInHierarchy == true && hasDeactivated == false)
        {
            DeActivateAttackPoints();
            hasDeactivated = true;

            return;
        }
    }


    private void SetInitialVariables()
    {
        SetManagers();
        GetCrewReputation();
        SetGameMode();
    }


    #region Get Managers

    public CrewBeefCakeManager GetBeefcakeManager() 
    {
        return bcm;
    }

    public CombatStatManager GetCombatStatManager()
    {
        return csm;
    }

    public AttackAnimationManager GetAttackAnimationManager() 
    {
        return aam;
    }

    public CarManager GetCarManager() 
    {
        return cm;
    }

    public CrewInventory GetCrewInventory() 
    {
        return crewInventory;
    }

    public SoundEffectManager GetSoundEffectManager()
    {
        return sem;
    }

    public JuiceManager GetJuiceManager()
    {
        return jm;
    }

    public CoinManager GetCoinManager()
    {
        return coinM;
    }

    #endregion


    #region Set Managers

    private void SetManagers() {
        SetBeefcakeManager();
        SetCombatStatManager();
        SetAttackAnimationManager();
        SetCarManager();
        SetCrewInventory();
        SetSoundEffectManager();
        SetJuiceManager();
        SetCoinManager();
        CreateInstantsOfMusicManager();
    }

    private void SetBeefcakeManager()
    {
        bcm = FindObjectOfType<CrewBeefCakeManager>();
    }

    private void SetCombatStatManager()
    {
       csm = FindObjectOfType<CombatStatManager>();
    }

    private void SetAttackAnimationManager()
    {
        aam = FindObjectOfType<AttackAnimationManager>();
    }

    private void SetCarManager()
    {
        cm = FindObjectOfType<CarManager>();
    }

    private void SetCrewInventory()
    {
        crewInventory = FindObjectOfType<CrewInventory>();
    }
    private void SetSoundEffectManager()
    {
        sem = FindObjectOfType<SoundEffectManager>();
    }

    private void SetJuiceManager()
    {
        jm = FindObjectOfType<JuiceManager>();
    }

    private void SetCoinManager()
    {
        coinM = FindObjectOfType<CoinManager>();
    }

    private void CreateInstantsOfMusicManager()
    {
        if (MusicManager.musicManagerInstance == null)
        {
            Instantiate(musicManager);
        }
        
    }
    #endregion

    private void GetCrewReputation()
    {
        CurrentCrewData currentCrewData = Resources.Load<CurrentCrewData>("DynamicData/CrewData/CurrentCrewData");
        currentCrewReputation = currentCrewData.currentCrewReputation;
    }

    private void SetCrewReputation(int newReputationScore)
    {
        var currentCrewData = Resources.Load<CurrentCrewData>("DynamicData/CrewData/CurrentCrewData");
        currentCrewData.currentCrewReputation = newReputationScore;
    }


    private void SetGameMode()
    {
        if (gameObject.name != "FixLoop_GameManager") return;

        // If there's a JobData file then set game mode to {Job}
        job = Resources.Load<JobData>("DynamicData/JobData/JobData");
        if (job != null)
            gameMode = GameMode.Job;
        else
            gameMode = GameMode.Endless;
    }


    public int GetCrewCurrentReputation()
    {
        return currentCrewReputation;
    }

    private void AddCurrentAttackPointsToList() 
    {
        foreach (GameObject attackPoint in GameObject.FindGameObjectsWithTag("FixPoint"))
        {
            csm.currentAttackPoints.Add(attackPoint);
        }
    }


    private void DeActivateAttackPoints()
    {
        AddCurrentAttackPointsToList();
        foreach (GameObject attackPoint in csm.currentAttackPoints)
        {
            attackPoint.gameObject.SetActive(false);
        }
    }

    private void ActivateAttackPoints()
    {
        
        foreach (GameObject attackPoint in csm.currentAttackPoints)
        {
            attackPoint.gameObject.SetActive(true);
        }

        csm.currentAttackPoints.Clear();
    }
}

public static class Helper
{
    public static void SlerpTransform(this Transform t1, Transform t2, float t)
    {
        t1.position = Vector3.Slerp(t1.position, t2.position, t);
        t1.rotation = Quaternion.Slerp(t1.rotation, t2.rotation, t);
        t1.localScale = Vector3.Slerp(t1.localScale, t2.localScale, t);
    }
}