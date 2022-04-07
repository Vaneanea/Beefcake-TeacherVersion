using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        //Create slots 
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
        slot.Set(crewItem);
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

    private void GetCrew() 
    {
        //string[] lookFor = new string[] { "Assets/Resources/Data/CrewBeefCakes" };
        //string[] yourBeefcakes = AssetDatabase.FindAssets("t:" + typeof(CrewBeefCake).Name, lookFor);

        //foreach (string beefCakeName in yourBeefcakes)
        //{
        //    var SOpath = AssetDatabase.GUIDToAssetPath(beefCakeName);
        //    var beefCake = AssetDatabase.LoadAssetAtPath<CrewBeefCake>(SOpath);
        //    crew.Add(beefCake);
        //}

        //string lookFor = "Data/CrewBeefCakes";
        //CrewBeefCake[] yourBeefcakes = (CrewBeefCake[])Resources.LoadAll(lookFor);

        List<CrewBeefCake> yourBeefcakes = SaveManager.crew;
        foreach (CrewBeefCake beefCake in yourBeefcakes)
        {
           crew.Add(beefCake);
        }

    }

    private CrewBeefCake CreateNewCrewBeefCake(ShopBeefCake data) {

        CrewBeefCake beefCake = ScriptableObject.CreateInstance<CrewBeefCake>();

        beefCake.source = data.source;

        beefCake.stamina = data.stamina;
        beefCake.strength = data.strength;
        beefCake.speed = data.speed;
        beefCake.characterPrefab = data.source.characterPrefab;
        beefCake.headshot = data.source.headshot;
        beefCake.bgColor = data.source.bgColor;
        beefCake.isFatigued = false;

        beefCake.displayName = data.source.displayName;


        return beefCake;
    }

    private void SaveCrewBeefCake(CrewBeefCake beefCake) 
    {
        //string path = "Assets/Resources/Data/CrewBeefCakes/" + beefCake.displayName + ".asset";
        //AssetDatabase.CreateAsset(beefCake, path);

        SaveManager.crew.Add(beefCake);
    }

    // TODO: add Remove and Get methods
}
