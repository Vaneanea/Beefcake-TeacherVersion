using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem 
{
    public const string SAVES_PATH = "/saves";
    public static void Save<T>(T obj, string key)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVES_PATH;
        
        Directory.CreateDirectory(path);

        FileStream stream = new FileStream(path + key, FileMode.Create);
        formatter.Serialize(stream, obj);
        stream.Close();
    }


    public static string[] GetCrewCount()
    {
        string path = Application.persistentDataPath + SAVES_PATH;
        string[] filePaths = Directory.GetFiles(path);
        return filePaths;
    }
    public static T Load<T>( string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVES_PATH;
        T data = default;

        if (File.Exists(path + key))
        {
            FileStream stream = new FileStream(path + key, FileMode.Open);
            data = (T)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.Log("Save not found in "+ path + key);
        }

        return data;

    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + SAVES_PATH;
        return File.Exists(path + key);
    }


}
