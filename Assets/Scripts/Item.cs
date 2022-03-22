using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Represents an item that is present in ItemInventory 
// Concrete instance of ItemData with extra data and methods
public class Item : ScriptableObject {
    public ItemData source;
  
    // Acts as the constructor, needs to be called when creating a new instance 
    public void OnCreated(ItemData source) {
        this.source = source;

        // Save new instance as an asset to be loaded up later
        // TODO: This path might need to be changed 
        string path = "Assets/Resources/Item Inventory/" + source.displayName + ".asset";
        AssetDatabase.CreateAsset(this, path);
    }

    // TODO: add other relevant data or methods 
}
