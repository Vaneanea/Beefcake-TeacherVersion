using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeefCakeManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerBeefcake;
    public Transform[] startingPositions;
    private CrewBeefCake[] crewBeefcakes;
    public GameObject BeefcakeHolder;
    

    //make a method that throws the crewinventory to the fix scene

    // Start is called before the first frame update
    void Start()
    {
        //set crewlist to array of beefcakes
        crewBeefcakes = CrewInventory.instance.GetCrewList().ToArray();

        //Debug.Log(crewBeefcakes.Length);

        for (int i = 0; i <= crewBeefcakes.Length -1 ; i++) {

            CreateBeefcakeGameObject(crewBeefcakes[i],i);
            
        }

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreateBeefcakeGameObject(CrewBeefCake beefcakedata, int index)
    {
        GameObject beefcake = Instantiate(BeefcakeHolder);
        beefcake.transform.position = startingPositions[index].position;
        beefcake.transform.rotation= startingPositions[index].rotation;
        beefcake.GetComponent<BeefCake>().beefCake = beefcakedata;
        beefcake.name = beefcakedata.source.displayName;

        Instantiate(beefcakedata.source.characterPrefab, beefcake.transform);

        //connecting slot data
        GameObject slot = GameObject.Find(beefcakedata.displayName + " Slot");
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetMaxHealth(beefcake.GetComponent<BeefCake>().beefCake.stamina);
        beefcake.GetComponent<BeefCake>().beefCake.currentStamina= beefcake.GetComponent<BeefCake>().beefCake.stamina;
        beefcake.GetComponent<BeefCake>().beefCake.isFatigued = false;

        //Debug.Log(slot.transform.GetChild(2).GetComponentInChildren<TextMesh>().text);
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetHealth(beefcake.GetComponent<BeefCake>().beefCake.currentStamina);
        slot.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text= beefcake.GetComponent<BeefCake>().beefCake.strength.ToString();
        slot.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = beefcake.GetComponent<BeefCake>().beefCake.speed.ToString();

        if (index == 0)
        {
            //assign first eefcake as player 
            crewBeefcakes[index].isPlayer = true;
            playerBeefcake = beefcake;

        }
        

    }

    public GameObject GetPlayerBeefcake() {

        return playerBeefcake;
    
    }
    
}
