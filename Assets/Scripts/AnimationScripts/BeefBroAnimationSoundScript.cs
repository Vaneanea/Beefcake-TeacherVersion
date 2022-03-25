using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefBroAnimationSoundScript : MonoBehaviour
{
    #region Player Attack Animation Events
   
    private void Punch_Contact_1()
    {
        SoundEffectManager.Play("Punch1");
    }

    private void Punch_Contact_2()
    {
        SoundEffectManager.Play("Punch2");
    }
    private void Kick_Contact2()
    {
        SoundEffectManager.Play("Kick2");
    }


    private void Kick_Contact1()
    {
        SoundEffectManager.Play("Kick1");
    }



 

    #endregion

}
