using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefBroAnimationSoundScript : MonoBehaviour
{
    [Header("Private Managers")]
    private GameManager gm;
    private JuiceManager jm;
    private CarManager cm;

    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
    }

    #region Set Managers
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        jm = gm.GetJuiceManager();
        cm = gm.GetCarManager();

    }
    #endregion

    #region Player Attack Animation Events

    private void Punch_Contact_1()
    {
        SoundEffectManager.Play("Punch1");

        if (cm.GetCar() != null)
        {
            jm.ShakeCar();
        }
        
    }

    private void Punch_Contact_2()
    {
        SoundEffectManager.Play("Punch2");
        if (cm.GetCar().GetComponent<Car>().hasLanded == true)
        {
            jm.ShakeCar();
        }
    }
    private void Kick_Contact2()
    {
        SoundEffectManager.Play("Kick2");
        if (cm.GetCar().GetComponent<Car>().hasLanded == true)
        {
            jm.ShakeCar();
        }
    }

    private void Kick_Contact1()
    {
        SoundEffectManager.Play("Kick1");
        if (cm.GetCar().GetComponent<Car>().hasLanded == true)
        {
            jm.ShakeCar();
        }
    }

    #endregion

}
