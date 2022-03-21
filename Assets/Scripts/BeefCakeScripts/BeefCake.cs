using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefCake : MonoBehaviour
{
    public CrewBeefCake beefCake;
    private GameObject slot;
    private SliderScript staminaSlider;

    void Start()
    {
        SetSlot();
        SetSlider();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFatigued();
    }

    #region Initial Set Methods
    private void SetSlot()
    {
        slot = GameObject.Find(beefCake.displayName + " Slot");
    }

    private void SetSlider()
    {
        staminaSlider = slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>();
    }
    #endregion

    #region Stamina Control
    public void ReduceStamina(int staminaReduction)
    {
        beefCake.currentStamina -= staminaReduction;
        SetStamina();
    }

    public void IncreaseStamina(int staminaIncrease)
    {
        beefCake.currentStamina += staminaIncrease;
        SetStamina();
    }

    private void SetStamina() 
    {
        staminaSlider.SetHealth(beefCake.currentStamina);
    }
    #endregion

    public int GetAttackDamage()
    {
       return  beefCake.strength;
    }
       
    private void CheckIfFatigued() 
    {
        if (beefCake.currentStamina <= 0)
        {
            beefCake.isFatigued = true;
        }
    }
}
