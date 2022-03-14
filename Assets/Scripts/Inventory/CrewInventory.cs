using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the crew members that are available to the player
// Singleton
public class CrewInventory : MonoBehaviour {
    public static CrewInventory instance { get; private set; }

    [SerializeField] private List<CrewBeefCake> crew;
    
    [Header("Inventory Display Variables")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject inventoryObject;

    private void Awake() {
        crew = new List<CrewBeefCake>();

        // Handle Singleton instance creation
        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        // TODO: Display BeefCakes that are already in inventory
    }

    public void Add(BeefCakeData data) {
        CrewBeefCake beefCake = new CrewBeefCake(data);
        crew.Add(beefCake);

        AddInventorySlot(beefCake);
    }

    private void AddInventorySlot(CrewBeefCake crewItem) {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(inventoryObject.transform);
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(crewItem);
    }

    // TODO: add Remove and Get methods
}
