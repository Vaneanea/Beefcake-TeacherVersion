using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingPoint : MonoBehaviour
{
    public GameObject Item;
    public GameObject currentTarget;
    public ParticleSystem smokeParticle;
    public int maxFix;
    public int currentFix;
    //how much is fixed per tap
    public int tapFix;

    //reference to the healthbar script
    public HealthBar healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        currentFix = 0;
        //adjust the max health
        healthBar.SetMaxHealth(maxFix);
        //adjust the current health
        healthBar.SetHealth(currentFix);
    }

    // Update is called once per frame
    private void Update()
    {

        #region - mouse input -
        ////click on object to decrease the healthbar/fixing bar
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //to see if the mouse hits anything
        //    RaycastHit hit;
        //    //calculate the rays position on the screen
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //calculate the distance of the ray
        //    if(Physics.Raycast(ray, out hit, 1000.0f))
        //    {
        //        // check if the mouse clicks on the object that is it's targete game object
        //        if (hit.transform.gameObject == currentTarget)
        //        {
        //            //play animations
        //            //play attack animation
        //            Item.GetComponent<AttackButtonScript>().Attack();
        //            //play particle effect
        //            //play breaking sounds

        //            //increase the current fix
        //            currentFix += tapFix;

        //            //set current fix values on the health bar
        //            healthBar.SetHealth(currentFix);
        //        }
        //    }
        //}
        #endregion

        #region - touch input -
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        //play animations
        //        //play attack animation
        //        Item.GetComponent<AttackButtonScript>().Attack();
        //        //play particle effect
        //        //play breaking sounds

        //        //increase the current fix
        //        currentFix += tapFix;

        //        //set current fix values on the health bar
        //        healthBar.SetHealth(currentFix);
        //    }
        //}
    #endregion


        //after the current fix bar reaches the end, apply fixing to the parent object so that it can change meshes after reachign its value
        if (currentFix >= maxFix)
        {
            Item.GetComponent<FixingObject>().fixItem();
            Destroy(this.gameObject);
        }
    }

    public void attackTarget()
    {
        //play animations
        //play attack animation
        Item.GetComponent<AttackButtonScript>().Attack();
        //play particle effect
        Instantiate(smokeParticle, transform.position, transform.rotation);
        //play breaking sounds

        //increase the current fix
        currentFix += tapFix;

        //set current fix values on the health bar
        healthBar.SetHealth(currentFix);
    }
}
