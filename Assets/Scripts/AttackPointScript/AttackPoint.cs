using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    //where the player stands when attacking this attackpoint
    public Transform PlayerPosition;

    private int maxHits;
    private int currentHits;

    [SerializeField]
    private GameManager gm;

    [SerializeField]
    private CombatStatManager csm;

    [SerializeField]
    private BeefCakeManager bcm;

    [SerializeField]
    private BeefCake player;

    //reference to the healthbar script
    public SliderScript healthBar;

    //public ParticleSystem smokeParticle;

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


    private void SetInitialParameters() {
        SetGameManager();
        SetOtherManagers();
        SetPlayer();
        SetAttackPointInitialHealth();
        SetPlayerPosition();
    }


    #region Inital Set Methods
    private void SetGameManager() {

        gm = FindObjectOfType<GameManager>();

    }

    private void SetOtherManagers()
    {
        csm = gm.GetCombatStatManager();
        bcm = gm.GetBeefcakeManager();
    }

    private void SetPlayer() 
    {
        player = bcm.GetPlayerBeefcake().GetComponent<BeefCake>();   
    }

   
    private void SetAttackPointInitialHealth()
    {
        //get maxhit amount from combat manager
        maxHits = gm.transform.GetChild(1).GetComponent<CombatStatManager>().attackPointHitMax;
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
        PlayerPosition = transform.GetChild(0).transform;
    }

    #endregion


    private void UpdateAttackPointHealthBarVisual() {

        healthBar.SetHealth(currentHits);
    }

    private void MovePlayerToAttackPosition()
    {
        //Put player in correct position to attack 
        gm.GetComponentInChildren<BeefCakeManager>().GetPlayerBeefcake().transform.localPosition = PlayerPosition.localPosition;
        gm.GetComponentInChildren<BeefCakeManager>().transform.rotation = PlayerPosition.rotation;
    }


    private void CheckIfDestroyed() {

        if (currentHits <= 0)
        {
            //Register attackpoint as fixed
            FindObjectOfType<Car>().FixAttackPoint();

            //Destory the attackpoint
            Destroy(gameObject);
        }
    }

    private void PlayAttackAnimation() {
        //play attack animation
        gm.GetComponentInChildren<AttackAnimationManager>().Attack();
    }

    private void ExecuteAttack() {

        //reduce current hits
        currentHits -= player.GetAttackDamage();

        //reduce stamina of player
        player.ReduceStamina(csm.staminaDecreaseValue);

    }


}
