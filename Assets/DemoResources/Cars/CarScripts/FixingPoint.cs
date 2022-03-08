using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingPoint : MonoBehaviour
{
    
    public Transform PlayerPosition;

    public ParticleSystem smokeParticle;
    public int maxHits = 15;
    public int currentHits;

    [SerializeField]
    private GameManager gm;
    

    //reference to the healthbar script
    public HealthBar healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        currentHits = 0;
        //adjust the max health
        healthBar.SetMaxHealth(maxHits);
        //adjust the current health
        healthBar.SetHealth(currentHits);
        PlayerPosition = transform.GetChild(0).transform;
       

    }

    // Update is called once per frame
    private void Update()
    {

        //after the current fix bar reaches the end, apply fixing to the parent object so that it can change meshes after reachign its value
        if (currentHits >= maxHits)
        {
             GetComponentInParent<CarHolder>().FixItem();
            
             Destroy(this.gameObject);
        }
    }

    public void attackTarget()
    {
        //Put player in correct position to attack 
        gm.playerBeefcake.transform.position = new Vector3(PlayerPosition.position.x, 0,PlayerPosition.position.z);
        gm.playerBeefcake.transform.rotation = PlayerPosition.rotation;

       
    
        //play animations
        //play attack animation
        gm.GetComponentInChildren<AttackAnimationManager>().Attack();
        //play particle effect
        //Instantiate(smokeParticle, transform.position, transform.rotation);
        //play breaking sounds

        //increase the current fix
        currentHits += gm.playerBeefcake.GetComponent<BeefBro>().GetStrenghtLevel();

        //reduce stamina
        gm.playerBeefcake.GetComponent<BeefBro>().ReduceStamina(1);

        //set current fix values on the health bar
        healthBar.SetHealth(currentHits);
      

    }
}
