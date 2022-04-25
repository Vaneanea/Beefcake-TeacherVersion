using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //Managers
    private GameManager gm;
    private CarManager cm;

    public Rigidbody coinPrefab;

    [Range(10, 100)]
    public float minSpeed;
    [Range(10, 100)]
    public float maxSpeed;

    [SerializeField]
    private List<Transform> spawnPoints;

    private int previousSpawnPoint;

    [SerializeField]
    private int coinsEarned = 0;

    [SerializeField]
    private int coinsCaught = 0;


    private void Start()
    {
        SetGameManager();
        SetOtherManagers();
       spawnPoints = GetComponentsInChildren<Transform>().ToList();
       spawnPoints.Remove(transform);
       
    }


    void Update()
    {
        //if (input.getkeydown(keycode.space)) {
        //    spawncoin();
        //}
    }

    public void SpawnCoin()
    {
        var speed = Random.Range(minSpeed, maxSpeed);
        int spawnPointNumber  = GetSpawnPosition();


        while (spawnPointNumber == previousSpawnPoint)
        {
            spawnPointNumber = GetSpawnPosition();
        }

        previousSpawnPoint = spawnPointNumber;

        var myVectorRange = new Vector3(spawnPoints[spawnPointNumber].position.x, spawnPoints[spawnPointNumber].position.y, spawnPoints[spawnPointNumber].position.z);
       
        Rigidbody coin = Instantiate(coinPrefab, myVectorRange, Quaternion.identity);
        coin.velocity = transform.up * speed;
    }


    private int GetSpawnPosition()
    {
        int spawnPointNumber = Random.Range(0, spawnPoints.Count);

        return spawnPointNumber;
    }

    public void AddToCoinsCaught()
    {
        coinsCaught++;
    }

    //Is very simple right now, is open for expansion in the futore. Maybe 
    public void AddCoinsEarnedForCar(int carStarCount)
    {
        int coinsEarned =  carStarCount * 100;
        this.coinsEarned += coinsEarned;
        gm.coinsEarned = this.coinsEarned;
    }

    public int GetTotalCoinsEarned()
    {
        int carBonus = cm.carDone * 25;

        coinsEarned += carBonus;
        coinsEarned += (coinsCaught * 5);

        return coinsEarned;
    }

    private void SetGameManager()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void SetOtherManagers()
    {
        cm = gm.GetCarManager();
     
    }
}
