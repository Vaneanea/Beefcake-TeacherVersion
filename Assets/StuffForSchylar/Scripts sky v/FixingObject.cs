using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FixingObject : MonoBehaviour
{
    //Tapping value to fix
    public float hits;
    //Max tap values
    public float firstStageHitsNeeded, secondStageHitsNeeded;
    //current mesh of object
    //public Mesh[] meshes;

    public GameObject[] items;
    public Animator animator;
    public bool playAnimation;
    public int stagesDone;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<MeshFilter>().mesh = meshes[0];
        playAnimation = true;
        stagesDone = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region -input to fix -
        ////Tap to rise fixing float
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //add to fixing
        //    fixing++;

        //}
        #endregion

        //fixing float reaches a number
        if (hits >= firstStageHitsNeeded && stagesDone == 0)
        {
            //change the mesh of the object
            //GetComponent<MeshFilter>().mesh = meshes[1];
            items[0].SetActive(false);
            items[1].SetActive(true);

            hits = 0;
            animationPlayer();
            stagesDone = 1;
            

        }
        //fixing float reaches a number
        if (hits >= secondStageHitsNeeded && stagesDone == 1)
        {
            //change the mesh of the object
            //GetComponent<MeshFilter>().mesh = meshes[2];
            items[1].SetActive(false);
            items[2].SetActive(true);


            hits = 0;
            playAnimation = true;
            animationPlayer();
            stagesDone = 2;
            
        }
        
    }

    public void fixItem()
    {
        hits++;
    }

    public void animationPlayer()
    {
        if (playAnimation)
        {
            animator.SetTrigger("cheer");
            playAnimation = false;

        }
    }
}
