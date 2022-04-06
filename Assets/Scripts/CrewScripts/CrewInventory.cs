using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

// Holds the crew members that are available to the player
// Singleton
public class CrewInventory : MonoBehaviour {
    public static CrewInventory instance { get; private set; }

    
     public List<CrewBeefCake> crew;
    
     public string persisterName;

    [Header("Inventory Display Variables")]
     public GameObject slotPrefab;
     public GameObject inventoryObject;

    //protected void OnEnable()
    //{
    //    crew = new List<CrewBeefCake>();
    //    Object[] yourBeefcakes = Resources.LoadAll("Data/CrewBeefCakes", typeof(CrewBeefCake));

    //    foreach (CrewBeefCake beefCake in yourBeefcakes)
    //    {
    //        //var SOpath = AssetDatabase.GUIDToAssetPath(beefCakeName);
    //        //var beefCake = AssetDatabase.LoadAssetAtPath<CrewBeefCake>(SOpath);

    //        crew.Add(beefCake);
    //    }

    //    for (int i = 0; i < crew.Count; i++)
    //    {
    //        if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i)))
    //        {
    //            BinaryFormatter bf = new BinaryFormatter();
    //            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i), FileMode.Open);
    //            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), crew[i]);
    //            file.Close();

    //        }
    //        else
    //        {
    //            //Do Nothing
    //        }
    //    }
    //}

    //protected void OnDisable()
    //{
    //    for (int i = 0; i < crew.Count; i++)
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
    //        var json = JsonUtility.ToJson(crew[i]);
    //        bf.Serialize(file, json);
    //        file.Close();
    //    }

    //}


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
        //SaveCrewBeefCake(beefCake);
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

    //private void GetCrew() 
    //{
       
    //    ///TODO ADHUST THIS TO persistentDataPath
    //    //string[] lookFor = new string[] { "Assets/Resources/Data/CrewBeefCakes" };
    //    //string[] yourBeefcakes = AssetDatabase.FindAssets("t:" + typeof(CrewBeefCake).Name, lookFor);
    //    Object[] yourBeefcakes = Resources.LoadAll("Data/CrewBeefCakes", typeof(CrewBeefCake));

    //    foreach (CrewBeefCake beefCake in yourBeefcakes)
    //    {
    //        //var SOpath = AssetDatabase.GUIDToAssetPath(beefCakeName);
    //        //var beefCake = AssetDatabase.LoadAssetAtPath<CrewBeefCake>(SOpath);

    //        crew.Add(beefCake);
    //    }
    //}

    private void GetCrew()
    {
        //load data

        string path = Application.persistentDataPath + "/YourBeefCakes";
        DirectoryInfo d = new DirectoryInfo(path);
        foreach (var file in d.GetFiles())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(file.DirectoryName, FileMode.Open);
            CrewBeefCake beefCake = formatter.Deserialize(stream) as CrewBeefCake;
            stream.Close();
            crew.Add(beefCake);
        }



        ///TODO ADHUST THIS TO persistentDataPath
        //string[] lookFor = new string[] { "Assets/Resources/Data/CrewBeefCakes" };
        //string[] yourBeefcakes = AssetDatabase.FindAssets("t:" + typeof(CrewBeefCake).Name, lookFor);
        Object[] yourBeefcakes = Resources.LoadAll("Data/CrewBeefCakes", typeof(CrewBeefCake));

        foreach (CrewBeefCake beefCake in yourBeefcakes)
        {
            //var SOpath = AssetDatabase.GUIDToAssetPath(beefCakeName);
            //var beefCake = AssetDatabase.LoadAssetAtPath<CrewBeefCake>(SOpath);

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

        //save data
        BinaryFormatter formatter = new BinaryFormatter();
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "YourBeefCakes")))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "YourBeefCakes"));

        }
        string path = Application.persistentDataPath + "/YourBeefCakes/" + beefCake.displayName;
        Debug.Log("Saving at: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, beefCake);
        stream.Close();

        return beefCake;
    }

    private void SaveCrewBeefCake(CrewBeefCake beefCake) 
    {
        string path = "Assets/Resources/Data/CrewBeefCakes/" + beefCake.displayName + ".asset";
        AssetDatabase.CreateAsset(beefCake, path);
        
       // Resources.
    }

    


    // TODO: add Remove and Get methods
}
