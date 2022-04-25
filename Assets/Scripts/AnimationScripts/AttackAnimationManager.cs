using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackAnimationManager : MonoBehaviour
{
    [Header(" Attack Variables ")]
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    private int attack = 1;
    private Animator animator;

    bool attackBool = true;

    private void Update()
    {
        //this is for debugging
        animator = FindObjectOfType<GameManager>().transform.GetChild(2).GetComponent<CrewBeefCakeManager>().GetPlayerBeefcake().GetComponentInChildren<Animator>();
        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }
    }

    public void  Attack() {

        if (Time.time >= nextAttackTime) {
            switch (attack)
            {
                case 1:
                    DoAnimation_Temporary();
                    break;
                case 2:
                    DoAnimation_Temporary();
                    break;
                case 3:
                    DoAnimation_Temporary();
                    break;
                case 4:
                    DoAnimation_Temporary();
                    break;
                case 5:
                    DoAnimation_Temporary();
                    break;
                case 6:
                    DoAnimation_Temporary();
                    break;
                case 7:
                    DoAnimation_Temporary();
                     break;
                default:
                    DoAnimation_Temporary();
                    break;
            }
        }
    }

    private void DoAnimation_Temporary() {

        CheckIfAttackAnimationIsAlreadyPlaying();
        
    }


    private void CheckIfAttackAnimationIsAlreadyPlaying()
    {
        //while attacking don't play another attack animation,
        //this is something we should change a later to make the animations a bit more responsive
        if (attackBool)
        {
            SetAttackBoolean(true);

            return;
        }
        if (!attackBool)
        {
            SetAttackBoolean(false);
        }

        ResetAttackSpeed();
    }

    private void ResetAttackSpeed()
    {
        //Ressetting attack speed 
        nextAttackTime = Time.time + 1f / attackRate;

        if (attack != 7)
        {
            attack++;
        }
        else
        {
            attack = 1;
        }
    }

    private void SetAttackBoolean(bool attackBoolState)
    {
        animator.SetBool("AttackBool " + attack, attackBoolState);
        attackBool = !attackBoolState;
    }
 }
