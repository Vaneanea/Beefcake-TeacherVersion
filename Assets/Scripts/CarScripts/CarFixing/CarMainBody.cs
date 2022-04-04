using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMainBody : MonoBehaviour
{
    [SerializeField]
    private Car car;

    public Vector3 startPos;
    public Vector3 shakePos;
    public float timer;


    //Managers
    private GameManager gm;
    private JuiceManager jm;

    private void Awake()
    {
       
        
    }

    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
        SetCar();
        AssignStartPosition();
        StopAllCoroutines();
    }


    #region Setting Inital Variables
    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        jm = gm.GetJuiceManager();
    }

    private void SetCar()
    {
        car = gameObject.GetComponentInParent<Car>();
    }

    public void AssignStartPosition()
    {
        if(car.hasLanded == true)
        {
            startPos = transform.position;

        }
    }



    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        IgnoreCollisonOnDebrie(collision);

        PlaySFX(collision);


    }

    private void PlayCarLandingParticleEffect()
    {
        jm.cloudDropCar.Play(true);
    }

    private void PlayCarSmokeEffects()
    {
        jm.carSmokeEffects.Play(true);
    }

    private void PlaySoundEffectOnLanding()
    {
        SoundEffectManager.Play("quick_smash_003");
        car.SetHasLanded(true);
    }

    private void PlaySFX(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && car.hasLanded == false && car.currentCarStage == car.firstCarStage)
        {
            PlaySoundEffectOnLanding();
            PlayCarLandingParticleEffect();
            PlayCarSmokeEffects();

            //if (car.currentCarStage == car.firstCarStage)
            //{
               
            //}

        }


    }

    private void IgnoreCollisonOnDebrie(Collision collision)
    {
        if (collision.gameObject.CompareTag("Debrie"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }


    public IEnumerator Shake()
    {
        timer = 0f;
        while (timer < jm.time)
        {
            timer += Time.deltaTime;
            shakePos = startPos + (Random.insideUnitSphere * jm.distance);

            transform.position = shakePos;

            if (jm.delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(jm.delayBetweenShakes);
            }
            else
            {
                yield return null;
            }

        }

        transform.position = startPos;

    }


}
