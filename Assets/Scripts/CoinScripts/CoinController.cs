using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private Vector3 sphereRange;
    Vector3 myVectorRange;
    public Rigidbody coinPrefab;

   
    public float minSpeed;
    public float maxSpeed;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCoin();
        }
    }


    // Update is called once per frame
    void SpawnCoin()
    {
        var speed = Random.Range(minSpeed, maxSpeed);
        var myVectorRange = new Vector3(transform.position.x, transform.position.y, transform.position.z);
       
        Rigidbody coin = Instantiate(coinPrefab, myVectorRange, Quaternion.identity);
        coin.velocity = coin.transform.up * speed;
     }
}
