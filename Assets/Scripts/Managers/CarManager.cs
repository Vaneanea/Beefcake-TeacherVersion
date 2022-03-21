using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{

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

        if (carHolder.GetComponent<CarHolder>().isDone == true)
        {
            ///activate when washing is implemented
            ////for carwash, move camera to correct position//
            //GetComponentInParent<GameManager>().cam.GetComponent<CameraBehaviour>().isWashing = true;

            //if (carHolder.GetComponent<CarHolder>().isWashed == true)
            //{
            //    StartCoroutine(WaitASec(1));

            //}


            Destroy(carHolder);
            CreateCarHolder();
        }
    }

    void CreateCarHolder()
    {
        GameObject x = Instantiate(aCarHolder);

        x.GetComponent<CarHolder>().car = cars[Random.Range(0, cars.Length)];
        x.GetComponent<CarHolder>().canvas = GetComponentInParent<GameManager>().canvas;

        carHolder = x;

    }
      
}
