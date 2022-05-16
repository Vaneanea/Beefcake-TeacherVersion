using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// TODO for button timing:
// * modify currentHits in ExecuteAttack based on button scale
public class AttackPoint : MonoBehaviour
{
    private int maxHits;
    private int currentHits;

    //Managers
    private GameManager gm;
    private CombatStatManager csm;
    private CrewBeefCakeManager bcm;
    private AttackAnimationManager aam;
    private JuiceManager jm;
    private CoinManager coinM;

    //Player
    [SerializeField]
    private BeefCake player;

    //where the player stands when attacking this attackpoint
    public Transform playerPosition;
    private Transform currentPlayerPosition;

    //reference to the healthbar script
    public SliderScript healthBar;

    #region Pulsate Button variables
    [Header("Pulsate Button variables")]
    [SerializeField] private Vector2 scaleTimeBounds;
    [SerializeField] private float minScale;
    private static float scaleTime;
    #endregion

    private void Awake()
    {
        SetGameManager();
        SetOtherManagers();
        InitializeScaleTime();
    }

    private void Start()
    {
        SetInitialParameters();
        currentPlayerPosition = bcm.startingPositions[0];

        StartPulsating();
    }

    private void Update()
    {
        CheckIfDestroyed();
    }

    public void AttackTarget()
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
        coinM = gm.GetCoinManager();
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

    #region Attack Methods
    private void UpdateAttackPointHealthBarVisual()
    {
       healthBar.SetHealth(currentHits);
    }

    private void MovePlayerToAttackPosition()
    {
        //Put player in correct position to attack 
        player.transform.localPosition = playerPosition.localPosition;
        player.transform.transform.localRotation = playerPosition.localRotation;

        if (currentPlayerPosition.localPosition != player.transform.localPosition)
        {

            PlayDustParticleEffect();
            UpdateCurrentPlayerPositions();
        }
        else
        {
            UpdateCurrentPlayerPositions();
        }

    }

    private void PlayDustParticleEffect()
    {
        //Update particle effect position
        jm.teleportationDust.gameObject.transform.position = currentPlayerPosition.localPosition;

        //PlaySoundEffect
        var rn = Random.Range(0, 1);
        if (rn == 0)
        {
            SoundEffectManager.Play("Whoosh1");
        }
        else
        {
            SoundEffectManager.Play("Whoosh2");
        }


        //Play particle effect
        jm.teleportationDust.Play();
    }

    private void UpdateCurrentPlayerPositions()
    {
        currentPlayerPosition.localPosition = player.transform.localPosition;
        currentPlayerPosition.localRotation = player.transform.transform.localRotation;
    }

    private void CheckIfDestroyed()
    {
        if (currentHits <= 0)
        {
            //Register attackpoint as fixed
            FindObjectOfType<Car>().FixAttackPoint();

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
        // TODO: Here modify currentHits depending on timing of hit

        //show progress in progressbar
        csm.IncreaseProgress(player.GetAttackDamage());

        //reduce stamina of player
        player.ReduceStamina(csm.staminaDecreaseValue);

        //determine if a coin will spawn
        DetermineIfCoinWillSpawn(3);

        StartCoroutine(ChangeTargetImage());
    }

    private IEnumerator ChangeTargetImage()
    {
        //effect when hit
        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;
        transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);

        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        transform.GetChild(1).GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }

    private void DetermineIfCoinWillSpawn(int chance)
    {
        int coinWillSpawn = Random.Range(0, chance);
        
        if (coinWillSpawn == 1)
        {
            coinM.SpawnCoin();
        }
    }
    #endregion

    #region Pulsate Button Methods
    private void InitializeScaleTime() {
        scaleTime = Random.Range(scaleTimeBounds.x, scaleTimeBounds.y);
    }

    private void StartPulsating() {
        LeanTween.scale(gameObject, new Vector3(minScale, minScale, minScale), scaleTime).setLoopPingPong();
    }
    #endregion
}