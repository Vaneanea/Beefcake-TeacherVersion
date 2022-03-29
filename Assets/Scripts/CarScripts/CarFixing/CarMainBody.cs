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
        SetGameManager();
        SetOtherManagers();
    }

    private void Start()
    {
        
        SetCar();
        AssignStartPosition();
        StopAllCoroutines();
    }

   

    private void Update()
    {
        AssignStartPosition();
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

    private void AssignStartPosition()
    {
        if (car.hasLanded == true)
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

    private void PlaySoundEffectOnLanding() 
    {
        SoundEffectManager.Play("quick_smash_003");
        car.hasLanded = true;
    }

    private void PlaySFX(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && car.hasLanded == false)
        {
            PlaySoundEffectOnLanding();
            PlayCarLandingParticleEffect();
        }

    }

    private void IgnoreCollisonOnDebrie(Collision collision)
    {
        if (collision.gameObject.tag == "Debrie")
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
