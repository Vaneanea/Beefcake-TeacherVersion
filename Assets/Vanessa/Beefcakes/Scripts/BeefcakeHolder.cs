using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefcakeHolder : MonoBehaviour
{
    public CrewBeefCake beefCake;
    private GameObject slot;

    // Start is called before the first frame update

    void Start()
    {
       slot = GameObject.Find(beefCake.displayName + " Slot");

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentStamina);

        if (beefCake.currentStamina <= 0)
        {

            beefCake.isFatigued = true;

        }

    }

    public void ReduceStamina(int staminaReduction)
    {
        
        beefCake.currentStamina -= staminaReduction;
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetHealth(beefCake.currentStamina);
    }


    public void IncreaseStamina(int staminaIncrease)
    {
       
        beefCake.currentStamina += staminaIncrease;
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetHealth(beefCake.currentStamina);

    }


    public int GetAttackDamage()
    {
        
        return  beefCake.strength;
    }
}
