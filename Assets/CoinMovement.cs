using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    float rotSpeed;
    bool isGrounded = false;
    int rotationLife = 120;

    // Update is called once per frame
    void Update()
    {
        // rotate coin
        rotSpeed = 90; // degrees per second
        if (!isGrounded)
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        }

        if (rotationLife > 0) {
            rotationLife--;
        } else isGrounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
