using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static List<CrewBeefCake> crew = new List<CrewBeefCake>();
    const string CREW_KEY = "/crew";
    const string CREW_COUNT_KEY = "/crew.count";
    //const string CAR_KEY = "/cars";

    void Awake()
    {
        Load();
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
        string crewcountkey = CREW_COUNT_KEY;

        SaveSystem.Save(crew.Count, crewcountkey);

        for (int i = 0; i < crew.Count; i++)
        {
            SaveSystem.Save(crew[i], key + i);

        }
    }

    void Load()
    {
        string key = CREW_KEY;
        string crewcountkey = CREW_COUNT_KEY;

        int crewcount = SaveSystem.Load<int>(crewcountkey);

        for (int i = 0; i < crewcount; i++)
        {
            CrewBeefCake beefCake = SaveSystem.Load<CrewBeefCake>(key + i);
            crew.Add(beefCake);

        }
    }

}
