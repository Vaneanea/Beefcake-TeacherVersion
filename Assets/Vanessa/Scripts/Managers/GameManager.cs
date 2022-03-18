using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public Canvas canvas;
    public Camera cam;
    public CrewInventory crewInventory;
    
     

    // Start is called before the first frame update
    void Start()
    {
       
       

    }

    // Update is called once per frame
    void Update()
    {
        


    
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