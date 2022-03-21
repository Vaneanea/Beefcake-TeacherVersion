using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeefcakeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBeefcake;
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
        if (playerBeefcake.GetComponent<Beefcake>().beefCake.isFatigued  == true)
        {
            GetComponentInParent<GameManager>().canvas.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


    private void CreateBeefcakeGameObject(CrewBeefCake beefcakedata, int index)
    {
        GameObject beefcake = Instantiate(BeefcakeHolder);
        beefcake.transform.position = startingPositions[index].position;
        beefcake.transform.rotation= startingPositions[index].rotation;
        beefcake.GetComponent<Beefcake>().beefCake = beefcakedata;
        beefcake.name = beefcakedata.source.displayName;

        Instantiate(beefcakedata.source.characterPrefab, beefcake.transform);

        //connecting slot data
        GameObject slot = GameObject.Find(beefcakedata.displayName + " Slot");
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetMaxHealth(beefcake.GetComponent<Beefcake>().beefCake.stamina);
        beefcake.GetComponent<Beefcake>().beefCake.currentStamina= beefcake.GetComponent<Beefcake>().beefCake.stamina;
        beefcake.GetComponent<Beefcake>().beefCake.isFatigued = false;

        //Debug.Log(slot.transform.GetChild(2).GetComponentInChildren<TextMesh>().text);
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetHealth(beefcake.GetComponent<Beefcake>().beefCake.currentStamina);
        slot.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text= beefcake.GetComponent<Beefcake>().beefCake.strength.ToString();
        slot.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = beefcake.GetComponent<Beefcake>().beefCake.speed.ToString();

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
