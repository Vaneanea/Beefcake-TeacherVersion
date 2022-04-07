using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager musicManagerInstance;

    [Header("Music Clips")]
    public AudioClip fixLoopMusic;
    public AudioClip mainMenuMusic;

    [SerializeField]
    private string currentScene;

    void Awake()
    {
        //makes hte music manager persists between scenes
        DontDestroyOnLoad(gameObject);
        SetCurrentScene();
        CreateSingleton();
    }

    //private void Update()
    //{
    //    if(currentScene == "FixLoopVisualUpdate" || currentScene == "MainScene")
    //    SetCurrentScene();
    //}

    private void CreateSingleton()
    {
         if (musicManagerInstance != null && musicManagerInstance != this) Destroy(gameObject);
        else musicManagerInstance = this;
    }

    private void SetCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene().name;

        GetMusicForScene();
        gameObject.GetComponent<AudioSource>().Play();
    }
        
    private void GetMusicForScene()
    {
            switch (currentScene)
            {
                case "FixLoopVisualUpdate":
                gameObject.GetComponent<AudioSource>().clip = fixLoopMusic;
                  break;
                
                default:
                gameObject.GetComponent<AudioSource>().clip = fixLoopMusic;
                break;
            }
    }

}




