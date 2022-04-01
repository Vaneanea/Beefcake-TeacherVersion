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

    private BeefCakeManager bcm;
    private CombatStatManager csm;
    private AttackAnimationManager aam;
    private CarManager cm;
    private CrewInventory ci;
    private SoundEffectManager sem;
    private JuiceManager jm;

    private int currentCrewReputation;

    private bool hasDeactivated = false;

    public int coinsEarned;
    private bool earningsCalculated;

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
        if (gameObject.name == "FixLoop_GameManager" && gameMode == GameMode.Job) {
            if (cm.jobDone && !earningsCalculated)
                OnFixLoopEnd();
        }

            if (gameObject.name == "FixLoop_GameManager" && gameMode == GameMode.Endless) {

            if (bcm.playerBeefcake.GetComponent<BeefCake>().beefCake.isFatigued == true && earningsCalculated == false)
            {
                OnFixLoopEnd();
            }
                       
        }

        if (gameObject.name == "FixLoop_GameManager")
        {
            if (canvas.gameObject.transform.GetChild(1).gameObject.activeInHierarchy == true)
            {
                DeActivateAttackPoints();
                hasDeactivated = true;
            }

        }

       
        if (gameObject.name == "FixLoop_GameManager")
        {
            if (canvas.gameObject.transform.GetChild(1).gameObject.activeInHierarchy == false && hasDeactivated == true)
            {
                ActivateAttackPoints();
                hasDeactivated = false;

            }
        }


    }

    private void OnFixLoopEnd() {
        if (gameObject.name != "FixLoop_GameManager") return;

        // Show rewards pop-up
        var fatigueScreen = canvas.gameObject.transform.GetChild(0);
        cm.GetTotalCoinsEarned();
        fatigueScreen.gameObject.SetActive(true);
        coinReward.GetComponent<TextMeshProUGUI>().text = cm.GetTotalCoinsEarned().ToString();
        var specialGems = cm.carDone / 4;
        gemReward.GetComponent<TextMeshProUGUI>().text = Mathf.Round(specialGems).ToString();
        DeActivateAttackPoints();
        earningsCalculated = true;

    }

    private void SetInitialVariables()
    {
        SetManagers();
        GetCrewReputation();
        SetGameMode();
    }



    #region Get Managers

    public BeefCakeManager GetBeefcakeManager() 
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
        return ci;
    }

    public SoundEffectManager GetSoundEffectManager()
    {
        return sem;
    }

    public JuiceManager GetJuiceManager()
    {
        return jm;
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
        CreateInstantsOfMusicManager();
    }

    private void SetBeefcakeManager()
    {
        bcm = FindObjectOfType<BeefCakeManager>();
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
        ci = FindObjectOfType<CrewInventory>();
    }
    private void SetSoundEffectManager()
    {
        sem = FindObjectOfType<SoundEffectManager>();
    }

    private void SetJuiceManager()
    {
        jm = FindObjectOfType<JuiceManager>();
    }


    private void CreateInstantsOfMusicManager()
    {
        if (MusicManager.musicManagerInstance == null)
        {
            Instantiate(musicManager);
        }
        
    }
    #endregion

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