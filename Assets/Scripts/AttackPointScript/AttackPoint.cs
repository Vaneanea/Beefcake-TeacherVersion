using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private int maxHits;
    private int currentHits;

    //Managers
    private GameManager gm;
    private CombatStatManager csm;
    private BeefCakeManager bcm;
    private AttackAnimationManager aam;
    private JuiceManager jm;
    private CarManager cm;


    //Player
    [SerializeField]
    private BeefCake player;

    //where the player stands when attacking this attackpoint
    public Transform playerPosition;

    //reference to the healthbar script
    public SliderScript healthBar;



    //public ParticleSystem smokeParticle;

    private void Awake()
    {
        SetGameManager();
        SetOtherManagers();
    }

    private void Start()
    {
        SetInitialParameters();
    }

    private void Update()
    {
        CheckIfDestroyed();
    }

    public void attackTarget()
    {
        MovePlayerToAttackPosition();

        PlayAttackAnimation();

        ExecuteAttack();

        //set current fix values on the health bar
        UpdateAttackPointHealthBarVisual();
    }

    private void SetInitialParameters()
    {
        SetPlayer();
        SetAttackPointInitialHealth();
        SetPlayerPosition();
    }

    #region Set Managers & Player
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        csm = gm.GetCombatStatManager();
        bcm = gm.GetBeefcakeManager();
        aam = gm.GetAttackAnimationManager();
        jm = gm.GetJuiceManager();
        cm = gm.GetCarManager();

    }
    private void SetPlayer()
    {
        player = bcm.GetPlayerBeefcake().GetComponent<BeefCake>();
    }

    #endregion

    #region Inital Set Methods

    private void SetAttackPointInitialHealth()
    {
        //get maxhit amount from combat manager
        maxHits = csm.attackPointHitMax;
        currentHits = maxHits;

        SetAttackPointValuesToVisual();
    }

    private void SetAttackPointValuesToVisual()
    {
        //adjust the max health
        healthBar.SetMaxHealth(maxHits);
        //adjust the current health
        UpdateAttackPointHealthBarVisual();
    }

    private void SetPlayerPosition()
    {
        playerPosition = transform.GetChild(0).transform;
    }
    #endregion

    private void UpdateAttackPointHealthBarVisual()
    {
       healthBar.SetHealth(currentHits);
    }

    private void MovePlayerToAttackPosition()
    {
        //Put player in correct position to attack 
        player.transform.localPosition = playerPosition.localPosition;
        player.transform.transform.localRotation = playerPosition.localRotation;
    }

    private void CheckIfDestroyed()
    {
        if (currentHits <= 0)
        {
            //Register attackpoint as fixed
            cm.GetCar().GetComponent<Car>().FixAttackPoint();


            //Destory the attackpoint
            Destroy(gameObject);
        }
    }

    private void PlayAttackAnimation()
    {
        //play attack animation
        aam.Attack();
    }

    private void ExecuteAttack()
    {
        //reduce current hits
        currentHits -= player.GetAttackDamage();

        //show progress in progressbar
        csm.IncreaseProgress(player.GetAttackDamage());

        //reduce stamina of player
        player.ReduceStamina(csm.staminaDecreaseValue);

    }
}  