using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds item inventory for all types of items: Gym, Items, Equipment
// Singleton
public class ItemInventory : MonoBehaviour {
   public static ItemInventory instance { get; private set; }

    // TODO: Take into account item Type
    [SerializeField] private List<Item> inventory;

    private void Awake() {
        // Singleton instance creation
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }

        instance = this;

        // TODO: Load up already existing items 
        inventory = new List<Item>();
    }

    public void Add(ShopItem data) {
        // TODO: if Item already exist add to its stack size

        Item item = ScriptableObject.CreateInstance<Item>();
        item.OnCreated(data.source);

        inventory.Add(item);
    }
}
