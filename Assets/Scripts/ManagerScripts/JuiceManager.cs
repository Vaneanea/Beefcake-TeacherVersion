using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JuiceManager : MonoBehaviour
{

    [Header("Settings Car Shake")]
    [Range(0f, 2f)]
    public float time = 0.2f;
    [Range(0f, 2f)]
    public float distance = 0.1f;
    [Range(0f, 0.1f)]
    public float delayBetweenShakes = 0f;

   
    private GameManager gm;
    private CarManager cm;
    public CarMainBody car;



    [Header("Particles")]
    public ParticleSystem cloudDropCar;
    public ParticleSystem hitCarEffect;
    public ParticleSystem carSmokeEffects;
    public ParticleSystem teleportationDust;
    public GameObject crowdCheering;

    public int currentlyActiveSmokePillars;
        

    void Start()
    {
        SetGameManager();
        SetOtherManagers();
        SetCurrentActiveSmokePillars();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        car = cm.GetCar().gameObject.GetComponentInChildren<CarMainBody>();
    }

   
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        cm = gm.GetCarManager();
     
    }


    public void ShakeCar()
    {
       StartCoroutine(car.Shake());
    }

    public void SetCurrentActiveSmokePillars()
    {
        currentlyActiveSmokePillars = transform.GetChild(0).transform.GetChild(2).childCount;
    }

    public IEnumerator Cheer()
    {
        crowdCheering.SetActive(true);
        crowdCheering.GetComponent<Animator>().Play("cheering crowd");
        SoundEffectManager.Play("CrowdCheer");
        yield return new WaitForSeconds(1.5f);
        crowdCheering.SetActive(false);
    }

}
