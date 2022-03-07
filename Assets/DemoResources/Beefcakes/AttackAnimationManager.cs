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
    public Animator animator;

  
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
