using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform cameraPosition;
   

    // Start is called before the first frame update
    void Start()
    {
        
       
    }
    //Vector3(-10.5899992,1.50000012,-19.2199993)

    // Update is called once per frame
    void Update()
    { 
        
     
        //transform.SlerpTransform(cameraPosition, Time.deltaTime);


        //For when you use bro as brush
        //float dis = Vector3.Distance(cameraPosition.position, transform.position);

        //if (dis <= 1) {
        //    player.SetActive(true);
        //}
        
    } 

}

//public static class Helper
//{
//    public static void SlerpTransform(this Transform t1, Transform t2, float t)
//    {
//        t1.position = Vector3.Slerp(t1.position, t2.position, t);
//        t1.rotation = Quaternion.Slerp(t1.rotation, t2.rotation, t);
//        t1.localScale = Vector3.Slerp(t1.localScale, t2.localScale, t);
//    }
//}
