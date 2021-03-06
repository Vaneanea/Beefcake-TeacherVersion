using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static AudioClip carLandingSound;
    public static AudioClip punch1Sound;
    public static AudioClip punch2Sound;
    public static AudioClip kick1Sound;
    public static AudioClip kick2Sound;
    public static AudioClip teleWhoosh1;
    public static AudioClip teleWhoosh2;
    public static AudioClip carDrivingAway;
    public static AudioClip crowdCheering;
    public static AudioClip coin;

    static AudioSource audioSourceCarSounds;
    static AudioSource audioSourcePlayerAttackAudio;
    static AudioSource audioSourceMiscAudio;

    // Start is called before the first frame update
    void Start()
    {
        carLandingSound = Resources.Load<AudioClip>("Audio/Smash/quick_smash_003");
        punch1Sound = Resources.Load<AudioClip>("Audio/Punch/Punch1");
        punch2Sound = Resources.Load<AudioClip>("Audio/Punch/Punch2");
        kick1Sound = Resources.Load<AudioClip>("Audio/Kick/Kick1");
        kick2Sound = Resources.Load<AudioClip>("Audio/Kick/Kick2");
        teleWhoosh1 = Resources.Load<AudioClip>("Audio/Whoosh/Whoosh1");
        teleWhoosh2 = Resources.Load<AudioClip>("Audio/Whoosh/Whoosh2");
        carDrivingAway = Resources.Load<AudioClip>("Audio/Car/Car Brake Screech");
        crowdCheering = Resources.Load<AudioClip>("Audio/Crowds/crowd_applaud_01");
        coin = Resources.Load<AudioClip>("Audio/Coin/coin_drop");

        audioSourceCarSounds = transform.GetChild(0).GetComponent<AudioSource>();
        audioSourcePlayerAttackAudio = transform.GetChild(1).GetComponent<AudioSource>();
        audioSourceMiscAudio = transform.GetChild(2).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Play(string clip)
    {
        switch (clip) 
        {
            case "quick_smash_003":
                audioSourceCarSounds.PlayOneShot(carLandingSound);
                break;

            case "Punch1":
                audioSourcePlayerAttackAudio.PlayOneShot(punch1Sound);
                break;

            case "Punch2":
                audioSourcePlayerAttackAudio.PlayOneShot(punch2Sound);
                break;

            case "Kick1":
                audioSourcePlayerAttackAudio.PlayOneShot(kick1Sound);
                break;

            case "Kick2":
                audioSourcePlayerAttackAudio.PlayOneShot(kick2Sound);
                break;

            case "Whoosh1":
                audioSourceCarSounds.PlayOneShot(teleWhoosh1);
                break;

            case "Whoosh2":
                audioSourceCarSounds.PlayOneShot(teleWhoosh2);
                break;

            case "CarDrivingAway":
                audioSourceCarSounds.PlayOneShot(carDrivingAway);
                break;

            case "CrowdCheer":
                audioSourceCarSounds.PlayOneShot(crowdCheering);
                break;
            case "Coin":
                audioSourceCarSounds.PlayOneShot(coin);
                break;
        }
    }
}
