using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Represents an item that is present in ItemInventory 
// Concrete instance of ItemData with extra data and methods
public class Item : ScriptableObject {
    public ItemData source;

    [SerializeField] int stackSize;
   
    // Acts as the constructor, needs to be called when creating a new instance 
    public void OnCreated(ItemData source) {
        this.source = source;
        stackSize = 1;

        // Save new instance as an asset to be loaded up later
        string path = "Assets/Resources/Item Inventory/" + source.displayName + ".asset";
        AssetDatabase.CreateAsset(this, path);
    }

    public void AddToStack() {
        stackSize++;
    }

    public void RemoveFromStack() {
        stackSize--;

        // TODO: Handle stackSize becoming 0 (remove from Inventory ?)
    }
}
