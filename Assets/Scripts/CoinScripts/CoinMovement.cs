using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    float rotSpeed;
    public bool isGrounded = false;
    int rotationLife = 120;


    private void Start()
    {
        var carCollider = FindObjectOfType<GameManager>().transform.GetChild(4).GetComponent<CarManager>().GetCar().gameObject.GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), carCollider);
    }


    // Update is called once per frame
    void Update()
    {
        var carCollider = FindObjectOfType<GameManager>().transform.GetChild(4).GetComponent<CarManager>().GetCar().gameObject.GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), carCollider);

        // rotate coin
        rotSpeed = 90; // degrees per second
        if (!isGrounded)
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        }

        if (rotationLife > 0)
        {
            rotationLife--;
        }
        else isGrounded = true;

        if (isGrounded == true)
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (isGrounded == false)
    //    {
    //        var carCollider = FindObjectOfType<GameManager>().transform.GetChild(4).GetComponent<CarManager>().GetCar().gameObject.transform.GetChild(0).GetComponent<Collider>();

    //        if (collision.gameObject == carCollider)
    //        {
    //            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), carCollider);
    //        }
    //    }

    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isGrounded = true;
        }
    }

    //IEnumerator TempNoCollider() 
    //{
    //    gameObject.GetComponent<Collider>().enabled = false;
    //    yield return new WaitForSeconds(1);
    //    gameObject.GetComponent<Collider>().enabled = true;
    //}



}
