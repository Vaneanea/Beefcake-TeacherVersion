using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Serializable] public class CrewBeefCakeList : List<CrewBeefCake> { }


    public static CrewBeefCakeList crew = new CrewBeefCakeList();
    const string CREW_KEY = "/crew";
    //const string CAR_KEY = "/cars";

    void Awake()
    {
        Load();
        if (CrewInventory.instance != null)
        {
            CrewInventory.instance.GetCrew();
        }
        
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void OnApplicationPause(bool pause)
    {
        Save();
    }


    public void Save()
    {
        string key = CREW_KEY;
      
        for (int i = 0; i < crew.Count; i++)
        {
            SaveSystem.Save(new SaveGameBeefCake(crew[i]), key + i);

        }
    }

    void Load()
    {
        Save();

        string key = CREW_KEY;
             
        crew = new CrewBeefCakeList();
        for (int i = 0; i < SaveSystem.GetCrewCount().Length; i++)
        {
            SaveGameBeefCake saveBeefCake = SaveSystem.Load<SaveGameBeefCake>(key + i);

            crew.Add(CreateCrewBeefFromSave(saveBeefCake));
            //Debug.Log(CreateCrewBeefFromSave(saveBeefCake).displayName);

        }

        Save();
       
    }


    public CrewBeefCake CreateCrewBeefFromSave(SaveGameBeefCake saveBeefCake)
    {
        CrewBeefCake beefCake = ScriptableObject.CreateInstance<CrewBeefCake>();

        beefCake.visualID = saveBeefCake.visualID;
        beefCake.level = saveBeefCake.level;
        beefCake.displayName = saveBeefCake.displayName;
        beefCake.speed = saveBeefCake.speed;
        beefCake.strength = saveBeefCake.strength;
        beefCake.stamina = saveBeefCake.stamina;

        BeefCakeVisualData visualData = Resources.Load<BeefCakeVisualData>("Data/BeefCakeVisualData/" + beefCake.displayName);

        beefCake.characterPrefab = visualData.characterPrefab;
        beefCake.headshot = visualData.headshot;
        beefCake.bgColor = visualData.bgColor;

        beefCake.currentStamina = saveBeefCake.currentStamina;
        beefCake.isFatigued = saveBeefCake.isFatigued;
        beefCake.isPlayer = saveBeefCake.isPlayer;

        return beefCake;
    }

    [Serializable] 
    public class SaveGameBeefCake 
    {
        public int level;

        public string displayName;

        public int speed;
        public int strength;
        public int stamina;

        public int visualID;

        public int currentStamina;
        public bool isFatigued;
        public bool isPlayer;

        public SaveGameBeefCake(CrewBeefCake beefcake)
        {
            visualID = beefcake.visualID;
            level = beefcake.level;
            displayName = beefcake.displayName;
            speed = beefcake.speed;
            strength = beefcake.strength;
            stamina = beefcake.stamina;
            currentStamina = beefcake.currentStamina;
            isFatigued = beefcake.isFatigued;
            isPlayer = beefcake.isPlayer;
        }
    }

}

