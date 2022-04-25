using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoScript : MonoBehaviour
{
    public void CreateSlots() {

        List<CrewBeefCake> crew = CrewInventory.instance.GetCrewList();

        foreach (CrewBeefCake crewMember in crew)
        {

            CrewInventory.instance.AddInventorySlot(crewMember, this.gameObject, CrewInventory.instance.slotPrefab);

        }

    }
}
