using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerBeefcake;
    public Car[] cars;
    public GameObject aCarHolder;
    private GameObject carHolder;

    // Start is called before the first frame update
    void Start()
    {
        CreateCarHolder();
    }

    // Update is called once per frame
    void Update()
    {
        if (carHolder.GetComponent<CarHolder>().isDone == true) {

            Destroy(carHolder);
            Debug.Log("Yay");
            CreateCarHolder();
            Debug.Log("YahYah");

        }
    }


    void CreateCarHolder() {

        GameObject x = Instantiate(aCarHolder);
       
        x.GetComponent<CarHolder>().car = cars[Random.Range(0, cars.Length)];
       
        carHolder = x;
        
    }
}
