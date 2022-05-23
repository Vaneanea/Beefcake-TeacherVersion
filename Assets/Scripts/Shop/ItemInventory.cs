using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Holds item inventory for all types of items: Gym, Items, Equipment
// Singleton
public class ItemInventory : MonoBehaviour {
   public static ItemInventory instance { get; private set; }

    // TODO: Take into account item Type
    [SerializeField] private List<Item> inventory;

    private void Awake() {
        DontDestroyOnLoad(this);

        CreateSingletonInstance();

        inventory = new List<Item>();
    }

    public void Add(ShopItem data) {
        // If item already exist add to its stack size
        Item foundItem = inventory.Find(it => it.source == data.source);
        if (foundItem != null) {
            foundItem.AddToStack();
            return;
        }

        // Otherwise, create a new asset
        Item item = ScriptableObject.CreateInstance<Item>();
        item.OnCreated(data.source);

        inventory.Add(item);
    }

    private void CreateSingletonInstance() {
        // Singleton instance creation
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }
}
