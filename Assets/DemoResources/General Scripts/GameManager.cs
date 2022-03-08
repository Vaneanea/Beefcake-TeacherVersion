using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerBeefcake;
    public Car[] cars;
    public GameObject aCarHolder;
    private GameObject carHolder;
    public Canvas canvas;
    public Camera cam;
    public Transform washCamPosition;
    public Transform fixCamPosition;


    // Start is called before the first frame update
    void Start()
    {
        CreateCarHolder();

    }

    // Update is called once per frame
    void Update()
    {
        if (carHolder.GetComponent<CarHolder>().isDone == true) {

            //for carwash//
            cam.transform.SlerpTransform(washCamPosition, Time.deltaTime);

            if (carHolder.GetComponent<CarHolder>().isWashed == true)
            {
                StartCoroutine(WaitASec(1));

            }

        }

        

        if (playerBeefcake.GetComponent<BeefBro>().GetFatigue() == true) {

            canvas.gameObject.SetActive(true);
        }
    }


    void CreateCarHolder() {

        GameObject x = Instantiate(aCarHolder);
       
        x.GetComponent<CarHolder>().car = cars[Random.Range(0, cars.Length)];
       
        carHolder = x;
        
    }



    public IEnumerator WaitASec(int x)
    {
        yield return new WaitForSeconds(x);
        Destroy(carHolder);
        CreateCarHolder();
    }


}




public static class Helper
{
    public static void SlerpTransform(this Transform t1, Transform t2, float t)
    {
        t1.position = Vector3.Slerp(t1.position, t2.position, t);
        t1.rotation = Quaternion.Slerp(t1.rotation, t2.rotation, t);
        t1.localScale = Vector3.Slerp(t1.localScale, t2.localScale, t);
    }

   
}