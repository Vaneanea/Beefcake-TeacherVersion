using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMainBody : MonoBehaviour
{
    public bool hasLanded = false;

    private Vector3 startPos;
    private Vector3 shakePos;
    private float timer;

    //Managers
    private GameManager gm;
    private JuiceManager jm;

    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
    }

    private void Update()
    {
        if (hasLanded == true) 
        {
            startPos = transform.position;

        }
    }

    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        jm = gm.GetJuiceManager();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlaySoundEffectOnLanding(collision);
        IgnoreCollisonOnDebrie(collision);


    }

    private void PlaySoundEffectOnLanding(Collision collision) 
    {
        if (collision.gameObject.tag == "Floor" && hasLanded == false)
        {
            SoundEffectManager.Play("quick_smash_003");
            hasLanded = true;
        }
    }

    private void IgnoreCollisonOnDebrie(Collision collision)
    {
        if (collision.gameObject.tag == "Debrie")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    public void ShakeCar() 
    {
        StartCoroutine(Shake());
    }


    private IEnumerator Shake() 
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
