using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Holds the crew members that are available to the player
// Singleton
public class CrewInventory : MonoBehaviour {
    public static CrewInventory instance { get; private set; }

    [SerializeField] public List<CrewBeefCake> crew;
    
    [Header("Inventory Display Variables")]
     public GameObject slotPrefab;
     public GameObject inventoryObject;

    private void Awake() {

        CreateSingletonInstance();
        crew = new List<CrewBeefCake>();
        GetCrew();
       
    }

    private void Start()
    {
        inventoryObject.GetComponent<CharacterInfoScript>().CreateSlots();
    }

    public void Add(ShopBeefCake data) {
        CrewBeefCake beefCake = CreateNewCrewBeefCake(data);
        crew.Add(beefCake);
        SaveCrewBeefCake(beefCake);
        AddInventorySlot(beefCake, inventoryObject, slotPrefab);
    }

    public void AddInventorySlot(CrewBeefCake crewItem, GameObject inventoryObject, GameObject slotPrefab) {
        
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(inventoryObject.transform);
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        InventorySlot slot = obj.GetComponent<InventorySlot>();
        slot.Set(ref crewItem);
    }

    public List<CrewBeefCake> GetCrewList()
    {
        return crew;
    }

    private void CreateSingletonInstance() 
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void GetCrew() 
    {
        crew.Clear();
        List<CrewBeefCake> yourBeefcakes = SaveManager.crew;
        foreach (CrewBeefCake beefCake in yourBeefcakes)
        {
           crew.Add(beefCake);
        }

      
    }

    private CrewBeefCake CreateNewCrewBeefCake(ShopBeefCake data) {

        CrewBeefCake beefCake = ScriptableObject.CreateInstance<CrewBeefCake>();

        beefCake.level = data.level;

        beefCake.stamina = data.stamina;
        beefCake.strength = data.strength;
        beefCake.speed = data.speed;
        beefCake.visualID = data.visualID;
        beefCake.displayName = data.source.displayName;

        BeefCakeVisualData visualData = Resources.Load<BeefCakeVisualData>("Data/BeefCakeVisualData/" + beefCake.displayName);
       
        beefCake.characterPrefab = visualData.characterPrefab;
        beefCake.headshot = visualData.headshot;
        beefCake.bgColor = visualData.bgColor;

        beefCake.isFatigued = false;

        return beefCake;
    }

    private void SaveCrewBeefCake(CrewBeefCake beefCake) {
        SaveManager.crew.Add(beefCake);
    }
}
