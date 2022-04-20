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

    void Start()
    {
        GetCrewBeefCakes();
        CreateAllCrewBeefCakes();
    }

    private void CreateBeefcakeGameObject(CrewBeefCake beefcakedata, int index)
    {
       GameObject beefcake = CreateBeefCakeHolder(beefcakedata, index);

        CreateInventorySlot(beefcake, beefcakedata);

        if (index == 0)
        {
            //assign first beefcake as player 
            crewBeefcakes[index].isPlayer = true;
            playerBeefcake = beefcake;

        }

    }

    public GameObject GetPlayerBeefcake()
    {
        return playerBeefcake;
    }

    private void GetCrewBeefCakes()
    {
        crewBeefcakes = CrewInventory.instance.GetCrewList().ToArray();
    }

    private void CreateAllCrewBeefCakes()
    {
        for (int i = 0; i <= crewBeefcakes.Length - 1; i++)
        {
            CreateBeefcakeGameObject(crewBeefcakes[i], i);
        }
    }


    #region Create BeefCake Holder Methods
    private GameObject CreateBeefCakeHolder(CrewBeefCake beefcakedata, int index)
    {
        GameObject beefcake = Instantiate(BeefcakeHolder);
        SetPlayerBeefCakeInStartingPosition(beefcake, index);
        AssignBeefCakeWithData(beefcake, beefcakedata);
        NameBeefCakeGameObject(beefcake, beefcakedata);
        AssignBeefCake3DModel(beefcake, beefcakedata);

        return beefcake;
    }

    private void SetPlayerBeefCakeInStartingPosition(GameObject beefcake, int index)
    {
        beefcake.transform.position = startingPositions[index].position;
        beefcake.transform.rotation = startingPositions[index].rotation;
    }

    private void AssignBeefCakeWithData(GameObject beefcake, CrewBeefCake beefcakedata)
    {
        beefcake.GetComponent<BeefCake>().beefCake = beefcakedata;
    }

    private void NameBeefCakeGameObject(GameObject beefcake, CrewBeefCake beefcakedata)
    {
        beefcake.name = beefcakedata.displayName;
    }

    private void AssignBeefCake3DModel(GameObject beefcake, CrewBeefCake beefcakedata)
    {
        Instantiate(beefcakedata.characterPrefab, beefcake.transform);
    }
    #endregion

    private void CreateInventorySlot(GameObject beefcake, CrewBeefCake beefcakedata)
    {
        //connecting slot data
        GameObject slot = GameObject.Find(beefcakedata.displayName + " Slot");
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetMaxHealth(beefcake.GetComponent<BeefCake>().beefCake.stamina);
        beefcake.GetComponent<BeefCake>().beefCake.currentStamina = beefcake.GetComponent<BeefCake>().beefCake.stamina;
        beefcake.GetComponent<BeefCake>().beefCake.isFatigued = false;
        slot.transform.GetChild(4).transform.GetChild(0).GetComponent<SliderScript>().SetHealth(beefcake.GetComponent<BeefCake>().beefCake.currentStamina);
        slot.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = beefcake.GetComponent<BeefCake>().beefCake.strength.ToString();
        slot.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = beefcake.GetComponent<BeefCake>().beefCake.speed.ToString();
    }

    

}
