using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeefBro : MonoBehaviour
{
    //hits on Car per tap of the player
    [SerializeField]
    private int strenghtLevel = 1;
   

    //Tracks the stamina of the beefbro
    [SerializeField]
    private int Stamina = 100;

    public int CurrentStamina;

    private bool isFatigued;

    // Start is called before the first frame update
    void Start()
    {
       
        //adjust the max health
        GetComponentInChildren<HealthBar>().SetMaxHealth(Stamina);

        CurrentStamina = Stamina;

        //adjust the current health
        GetComponentInChildren<HealthBar>().SetHealth(CurrentStamina);

        isFatigued = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CurrentStamina);

        if (CurrentStamina <= 0) {

            isFatigued = true;

        }

    }


    public void ReduceStamina(int staminaReduction) {

       
        CurrentStamina -= staminaReduction;
        GetComponentInChildren<HealthBar>().SetHealth(CurrentStamina);

    }


    public void IncreaseStamina(int staminaIncrease)
    {

        CurrentStamina += staminaIncrease;
        GetComponentInChildren<HealthBar>().SetHealth(CurrentStamina);

    }


    public void SetStrenghtLevel(int strenghtLevel) {

        this.strenghtLevel = strenghtLevel;
    }

    public int GetStrenghtLevel()
    {

        return strenghtLevel;
    }

    public bool GetFatigue() {

        return isFatigued;
    }

}
