using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingPoint : MonoBehaviour
{
    public GameObject Item;
    public GameObject currentTarget;
    public int maxFix;
    public int currentFix;
    //how much is fixed per tap
    public int tapFix;

    //reference to the healthbar script
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentFix = 0;
        //adjust the max health
        healthBar.SetMaxHealth(maxFix);
        //adjust the current health
        healthBar.SetHealth(currentFix);
    }

    // Update is called once per frame
    void Update()
    {


        //click on object to decrease the healthbar/fixing bar
        if (Input.GetMouseButtonDown(0))
        {
            //to see if the mouse hits anything
            RaycastHit hit;
            //calculate the rays position on the screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //calculate the distance of the ray
            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                // check if the mouse clicks on the object that is it's targete game object
                if (hit.transform.gameObject == currentTarget)
                {
                    //increase the current fix
                    currentFix += tapFix;

                    //set current fix values on the health bar
                    healthBar.SetHealth(currentFix);
                }
            }
        }
        
        //after the current fix bar reaches the end, apply fixing to the parent object so that it can change meshes after reachign its value
        if (currentFix >= maxFix)
        {
            Item.GetComponent<FixingObject>().fixItem();
            Destroy(this.gameObject);
        }
    }
}
