using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackAnimationManager : MonoBehaviour
{
   
    [Header(" Attack Variables ")]
    //Attack variables
    
    
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    private int attack = 1;
    private Animator animator;

    private void Start()
    {
        
    }


    private void Update()
    {
        animator = FindObjectOfType<GameManager>().transform.GetChild(2).GetComponent<BeefcakeManager>().GetPlayerBeefcake().GetComponentInChildren<Animator>();
        if (Input.GetKeyDown(KeyCode.Space)) {

            Attack();

        }
    }

    public void  Attack() {

        if (Time.time >= nextAttackTime && attack == 1)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);
         
            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;

        }
        
        if (Time.time >= nextAttackTime && attack == 2)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);
            
          

            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;

            

        }


        if (Time.time >= nextAttackTime && attack == 3)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);



            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;



        }

        if (Time.time >= nextAttackTime && attack == 4)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);



            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;



        }

        if (Time.time >= nextAttackTime && attack == 5)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);



            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;



        }

        if (Time.time >= nextAttackTime && attack == 6)
        {
            //play animation 
            animator.SetTrigger("Attack " + attack);



            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack++;



        }

        if (Time.time >= nextAttackTime && attack == 7)
        {
            //play animation
            animator.SetTrigger("Attack " + attack);
            


            //Ressetting attack speed 
            nextAttackTime = Time.time + 1f / attackRate;

            attack = 1;
            


        }
    }


 
}
