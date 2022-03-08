using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarState : MonoBehaviour
{
    public bool hasLanded = false;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") 
        {
            hasLanded = true;
           
        }

    }

}
