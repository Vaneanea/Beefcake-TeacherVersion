using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMainBody : MonoBehaviour
{
    public bool hasLanded = false;

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

    public void Shake() 
    {
        
    
    } 

}
