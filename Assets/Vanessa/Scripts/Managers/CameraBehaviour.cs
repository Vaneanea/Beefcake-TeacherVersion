using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform fixCameraPosition;
    public Transform washCameraPosition;

    public bool isWashing;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = fixCameraPosition.position;
        transform.rotation = fixCameraPosition.rotation;

    }
   

    // Update is called once per frame
    void Update()
    {

        if (isWashing == true)
        {
            transform.SlerpTransform(washCameraPosition, Time.deltaTime);

        }
        else if (isWashing == false) {

            transform.SlerpTransform(fixCameraPosition, Time.deltaTime);

        }

        
     
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
