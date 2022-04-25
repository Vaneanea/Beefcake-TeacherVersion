using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    float rotSpeed;
    public bool isGrounded = false;
    int rotationLife = 120;
    private GameManager gm;
    private CarManager cm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        cm = gm.GetCarManager();
    }

    private void Start()
    {
        var carCollider = cm.GetCar().gameObject.GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), carCollider);
    }


    // Update is called once per frame
    void Update()
    {
        IgnoreCarCollision();
        RotateCoin();
        DestroyOnGroundImpact();
    }


    private void IgnoreCarCollision()
    {
        var carCollider = cm.GetCar().gameObject.GetComponentInChildren<Collider>();
        if (carCollider != null)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), carCollider);
        }

    }

    private void RotateCoin()
    {
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

    }

    private void DestroyOnGroundImpact()
    {
        //Destroy coin when it hit the floor
        if (isGrounded == true)
        {
            Destroy(gameObject);
        }
    }



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

}
