using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
            //CreateSlots();
     
       
    }

    // Update is called once per frame
    void Update()
    {
    
        
    }

    public void CreateSlots() {

        List<CrewBeefCake> crew = CrewInventory.instance.GetCrewList();

        foreach (CrewBeefCake crewMember in crew)
        {

            CrewInventory.instance.AddInventorySlot(crewMember, this.gameObject, CrewInventory.instance.slotPrefab);

        }

    }
}
