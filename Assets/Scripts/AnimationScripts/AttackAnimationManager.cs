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

    private void Update()
    {
        animator = FindObjectOfType<GameManager>().transform.GetChild(2).GetComponent<BeefCakeManager>().GetPlayerBeefcake().GetComponentInChildren<Animator>();
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
        //play animation 
        animator.SetTrigger("Attack " + attack);

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
 }
