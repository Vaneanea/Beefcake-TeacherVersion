using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public Camera cam;
    public CrewInventory crewInventory;

    private BeefCakeManager bcm;
    private CombatStatManager csm;
    private AttackAnimationManager aam;
    private CarManager cm;
    private CrewInventory ci;

    void Start()
    {
        SetManagers();
    }

    void Update()
    {
        if (gameObject.name == "FixLoop_GameManager") {

            if (GetComponentInChildren<BeefCakeManager>().playerBeefcake.GetComponent<BeefCake>().beefCake.isFatigued == true)
            {
                GetComponentInParent<GameManager>().canvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                GameObject[] attackPoints = GameObject.FindGameObjectsWithTag("FixPoint");

                foreach (GameObject attackPoint in attackPoints)
                {
                    Destroy(attackPoint);
                }
            }
        }
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

    #endregion


    #region Set Managers

    private void SetManagers() {
        SetBeefcakeManager();
        SetCombatStatManager();
        SetAttackAnimationManager();
        SetCarManager();
        SetCrewInventory();
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
    #endregion

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