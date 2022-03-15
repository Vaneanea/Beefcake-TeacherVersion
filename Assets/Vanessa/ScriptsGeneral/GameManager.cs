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
     

    // Start is called before the first frame update
    void Start()
    {
        CreateCarHolder();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            AttackAnimation();

        }


        if (carHolder.GetComponent<CarHolder>().isDone == true) {

            //for carwash, move camera to correct position//
            cam.GetComponent<MoveCamera>().isWashing = true;




            if (carHolder.GetComponent<CarHolder>().isWashed == true)
            {
                StartCoroutine(WaitASec(1));

            }

        }

        

        if (playerBeefcake.GetComponent<BeefBro>().GetFatigue() == true) {

            canvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


    void CreateCarHolder() {

        GameObject x = Instantiate(aCarHolder);
       
        x.GetComponent<CarHolder>().car = cars[Random.Range(0, cars.Length)];
        x.GetComponent<CarHolder>().canvas = canvas;

        carHolder = x;
        
    }



    public IEnumerator WaitASec(int x)
    {
        yield return new WaitForSeconds(x);
        Destroy(carHolder);
        CreateCarHolder();
    }

    public void AttackAnimation() {

        transform.GetChild(0).gameObject.GetComponent<AttackAnimationManager>().Attack();
    
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