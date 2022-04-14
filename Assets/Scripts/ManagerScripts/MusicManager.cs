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
    private string previousScene;

    bool musicShouldChange = false;
    

    void Awake()
    {
        //makes hte music manager persists between scenes
        DontDestroyOnLoad(gameObject);
        UpdateCurrentScene();
        CreateSingleton();
    }

     void Start()
    {
        GetMusicForScene();
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (UpdateCurrentScene() == true)
        {
            if (currentScene == "FixLoopVisualUpdate" && previousScene == "MainScene" || (previousScene == "FixLoopVisualUpdate" && currentScene == "MainScene"))
            {
                musicShouldChange = true;
            }
        }

        if (musicShouldChange == true)
        {
            GetMusicForScene();
            gameObject.GetComponent<AudioSource>().Play();
            musicShouldChange = false;
        }
           
    }

    private void CreateSingleton()
    {
         if (musicManagerInstance != null && musicManagerInstance != this) Destroy(gameObject);
        else musicManagerInstance = this;
    }

    private bool UpdateCurrentScene()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            previousScene = currentScene;
            currentScene = SceneManager.GetActiveScene().name;
            return true;
        }

        return false;

       
    }
        
    private void GetMusicForScene()
    {
            switch (currentScene)
            {
                case "FixLoopVisualUpdate":
                gameObject.GetComponent<AudioSource>().clip = fixLoopMusic;
                  break;
                
                default:
                gameObject.GetComponent<AudioSource>().clip = mainMenuMusic;
                break;
            }
    }

}




