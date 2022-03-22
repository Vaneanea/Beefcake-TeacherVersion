using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
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
        CheckIfWashing();

        //transform.SlerpTransform(cameraPosition, Time.deltaTime);
        //For when you use bro as brush
        //float dis = Vector3.Distance(cameraPosition.position, transform.position);

        //if (dis <= 1) {
        //    player.SetActive(true);
        //}
    }


    private void CheckIfWashing() 
    {
        if (isWashing == true)
        {
            transform.SlerpTransform(washCameraPosition, Time.deltaTime);
        }
        else if (isWashing == false)
        {
            transform.SlerpTransform(fixCameraPosition, Time.deltaTime);
        }
    }
}


