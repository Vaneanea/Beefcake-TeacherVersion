using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeefCake : MonoBehaviour
{
    private Touch touch;
    public new  Camera camera;
    private float speedModifier;
    public RaycastHit grabpoint;

    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {

            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

            touch = Input.GetTouch(0);

            if ((touch.phase == TouchPhase.Moved) && Physics.Raycast(ray, out grabpoint))
            {
                if (grabpoint.collider.gameObject.CompareTag("Beefcake")) 
                {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y + touch.deltaPosition.y * speedModifier, transform.position.z);
                }
            
            }
              
            
            //if (touch.phase == TouchPhase.Moved)
            //{

            //     transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier,
            //     transform.position.y + touch.deltaPosition.y * speedModifier, transform.position.z);
            //}

        }

        
    }

    //void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {

    //        // The pos of the touch on the screen
    //        Vector2 vTouchPos = Input.GetTouch(0).position;
    //        Ray ray = camera.ScreenPointToRay(vTouchPos);

    //        RaycastHit vHit;
    //        if (Physics.Raycast(ray.origin, ray.direction, out vHit))
    //        {
    //            if (vHit.transform.tag == "Beefcake")
    //            {
    //                touch = Input.GetTouch(0);
    //                if (touch.phase == TouchPhase.Moved)
    //                {

    //                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier,
    //                    transform.position.y + touch.deltaPosition.y * speedModifier, transform.position.z);

    //                }


    //            }
    //        }



    //    }

}
