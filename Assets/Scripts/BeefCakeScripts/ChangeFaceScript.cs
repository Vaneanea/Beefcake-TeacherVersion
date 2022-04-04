using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaceScript : MonoBehaviour
{

    public SkinnedMeshRenderer SMR;
    public Material[] faces;
    

    // Start is called before the first frame update
    void Start()
    {
        SMR = GetComponent<SkinnedMeshRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {

        //press this key in order for the face of the object ot change
        if (Input.GetKeyDown("space"))
        {
            SMR.material = faces[Random.Range(0, faces.Length)];
        }

    }


}
