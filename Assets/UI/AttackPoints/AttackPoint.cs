using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    
    public Transform PlayerPosition;

    //public ParticleSystem smokeParticle;
    private int maxHits;
    private int currentHits;

    [SerializeField]
    private GameManager gm;
    

    //reference to the healthbar script
    public SliderScript healthBar;

    // Start is called before the first frame update
    private void Start()
    {
       
        gm = FindObjectOfType<GameManager>();
        maxHits = gm.transform.GetChild(1).GetComponent<CombatStatManager>().attackPointHitMax;
        currentHits = maxHits;
        //adjust the max health
        healthBar.SetMaxHealth(maxHits);
        //adjust the current health
        healthBar.SetHealth(currentHits);
        PlayerPosition = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    private void Update()
    {

        //ask why this works
         //after the current fix bar reaches the end, apply fixing to the parent object so that it can change meshes after reachign its value
        if (currentHits <= 0)
        {
            FindObjectOfType<CarHolder>().FixItem();
            Destroy(gameObject);
        }
      
       
    }

    public void attackTarget()
    {
        //Put player in correct position to attack 
        gm.GetComponentInChildren<BeefcakeManager>().GetPlayerBeefcake().transform.localPosition = PlayerPosition.localPosition;
        gm.GetComponentInChildren<BeefcakeManager>().transform.rotation = PlayerPosition.rotation;
            
        //play animations
        //play attack animation
        gm.GetComponentInChildren<AttackAnimationManager>().Attack();
        //play particle effect
        //Instantiate(smokeParticle, transform.position, transform.rotation);
        //play breaking sounds

        //increase the current fix
        currentHits -= gm.GetComponentInChildren<BeefcakeManager>().GetPlayerBeefcake().GetComponent<Beefcake>().GetAttackDamage();
        

        //reduce stamina
        gm.GetComponentInChildren<BeefcakeManager>().GetPlayerBeefcake().GetComponent<Beefcake>().ReduceStamina(1);

        //set current fix values on the health bar
        healthBar.SetHealth(currentHits);
      

    }
}
