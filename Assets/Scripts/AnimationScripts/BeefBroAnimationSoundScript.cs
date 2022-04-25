using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefBroAnimationSoundScript : MonoBehaviour
{
    //Managers
    private GameManager gm;
    private JuiceManager jm;
    private CarManager cm;
    private CrewBeefCakeManager bcm;

    private ParticleSystem hitCarEffect;


    //Atacking Limbs
    private string leftFoot = "root/pelvis/thigh_l/calf_l/foot_l/ball_l/ball_l_02";
    private string rightFoot = "root/pelvis/thigh_r/calf_r/foot_r/ball_r";
    private string leftFist = "root/pelvis/spine_01/spine_02/spine_03/clavicle_l/upperarm_l/lowerarm_l/hand_l/middle_01_l";
    private string rightFist = "root/pelvis/spine_01/spine_02/spine_03/clavicle_r/upperarm_r/lowerarm_r/hand_r/middle_01_r";

    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
        SetParticleEffects();
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
        bcm = gm.GetBeefcakeManager();

    }
    #endregion

    private void SetParticleEffects()
    {
        if (gm.gameObject.name != "FixLoop_GameManager")
        {
            return;
        }

        hitCarEffect = jm.hitCarEffect;
    }

    private void PlayCarAttackSFX(string attackSoundName, string attackingLimb)
    {
            SoundEffectManager.Play(attackSoundName);
            if (cm.GetCar().GetComponent<Car>().hasLanded == true && jm.car != null)
            {
                jm.ShakeCar();
                hitCarEffect.transform.position = bcm.playerBeefcake.transform.GetChild(0).Find(attackingLimb).position;
                hitCarEffect.Play();
            }

    }


    #region Player Attack Animation Events

    private void BasicKick()
    {
        PlayCarAttackSFX("Kick1", leftFoot);
    }

    private void HurricaneKick()
    {
        PlayCarAttackSFX("Kick2", leftFoot);
    }

    private void LeftPunch()
    {
        PlayCarAttackSFX("Punch1", leftFist);
    }

    private void LowKick()
    {
        PlayCarAttackSFX("Kick2", rightFoot);
    }

    private void PunchCombo_l()
    {
        PlayCarAttackSFX("Punch2", leftFist);
    }

    private void PunchCombo_r()
    {
        PlayCarAttackSFX("Punch2", rightFist);
    }

    private void RoundHouseKick()
    {
        PlayCarAttackSFX("Kick1", rightFoot);
    }

    private void RightPunch()
    {
        PlayCarAttackSFX("Punch2", rightFist);
    }


    #endregion

}
