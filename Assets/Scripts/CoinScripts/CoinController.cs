using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    //private Vector3 sphereRange;
    //Vector3 myVectorRange;

    public Rigidbody coinPrefab;

    [Range(10, 100)]
    public float minSpeed;
    [Range(10, 100)]
    public float maxSpeed;

    [SerializeField]
    private Transform[] spawnPoints;


    private void Awake()
    {
       spawnPoints = GetComponentsInChildren<Transform>();
    }


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
        var spawnPointNumber = Random.Range(0, spawnPoints.Length);
        var myVectorRange = new Vector3(spawnPoints[spawnPointNumber].position.x, spawnPoints[spawnPointNumber].position.y, spawnPoints[spawnPointNumber].position.z);
       
        Rigidbody coin = Instantiate(coinPrefab, myVectorRange, Quaternion.identity);
        coin.velocity = transform.up * speed;
    }
}
