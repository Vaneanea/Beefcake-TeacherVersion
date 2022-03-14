using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds the crew members that are available to the player
// Singleton
public class CrewInventory : MonoBehaviour {
    public static CrewInventory instance { get; private set; }

    [SerializeField] private List<CrewBeefCake> crew;

    private void Awake() {
        crew = new List<CrewBeefCake>();

        // Handle Singleton instance creation
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void Add(BeefCakeData data) {
        CrewBeefCake beefCake = new CrewBeefCake(data);
        crew.Add(beefCake);
    }

    // TODO: add Remove and Get methods
}
