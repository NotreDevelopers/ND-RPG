using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [HideInInspector]
    private int transitonDoor;

    private Dictionary<int, int> levelEntranceMappings = null;          // Key = ID of exit, Value = ID of entrance
    private Dictionary<int, string> levelEntranceIndexMappings = null;  // Key = ID of entrance, Value = scene containing entrance
    private GameObject playerRef;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
            transitonDoor = 2;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
            return;
        }

        transitonDoor = instance.transitonDoor;

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);

        if(levelEntranceMappings == null)
        {
            //Create
            levelEntranceMappings = new Dictionary<int, int>();

            //Populate
            levelEntranceMappings.Add(0, 1);    // Default entrance point

            levelEntranceMappings.Add(2, 3);    // Bidirectional scene link
            levelEntranceMappings.Add(3, 2);    
        }

        if(levelEntranceIndexMappings == null)
        {
            //Create
            levelEntranceIndexMappings = new Dictionary<int, string>();

            //Populate
            levelEntranceIndexMappings.Add(1, "Test Scene");
            levelEntranceIndexMappings.Add(2, "Test Scene");
            levelEntranceIndexMappings.Add(3, "Test_Scene_kdavis");
        }

        playerRef = GameObject.Find("Player");

        //Call the InitGame function to initialize the first level
        InitGame();
    }

    public void loadSceneFromDoor(int exitDoorID)
    {
        transitonDoor = levelEntranceMappings[exitDoorID];
        Debug.Log("transitionDoor = " + transitonDoor);
        SceneManager.LoadScene(levelEntranceIndexMappings[transitonDoor]);
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        // Set player position
        playerRef = GameObject.Find("Player");

        // Find entrance door
        string doorName = "door" + transitonDoor;
        Debug.Log("doorName set to " + doorName);

        GameObject doorRef = GameObject.Find(doorName);
        doorScript doorRefScript = doorRef.gameObject.GetComponent<doorScript>();

        Debug.Log("X Offset: " + doorRefScript.entranceOffsetX + " Y Offset: " + doorRefScript.entranceOffsetY);
        Vector3 relativePlayerPosition = new Vector3(doorRef.transform.position.x + doorRefScript.entranceOffsetX, doorRef.transform.position.y + doorRefScript.entranceOffsetY, 0.0f);
        playerRef.gameObject.transform.position = relativePlayerPosition;
    }

    //Update is called every frame.
    void Update()
    {

    }
}
