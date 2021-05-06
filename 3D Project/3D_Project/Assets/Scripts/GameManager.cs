using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {
    [System.NonSerialized] public GameObject deathScreen;
    [System.NonSerialized] public GameObject pauseScreen;
    [System.NonSerialized] public GameObject firstPersonPlayer;
    [System.NonSerialized] public PlayerInventory playerInventory;
    [System.NonSerialized] public Camera playerCamera;

    private bool isPaused = false;

    public static GameManager Instance { get; private set; }

    public Vector3 playerPos;
    public Quaternion playerRotation;
    public Quaternion cameraRotation;
    public float knifeCount;
    public bool objectiveComplete;

    // Start is called before the first frame update
    void Start() {
        //set the lock state and instance
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;

        //get the variables
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        firstPersonPlayer = GameObject.FindGameObjectWithTag("Player");
        playerCamera = firstPersonPlayer.GetComponentInChildren<Camera>();
        playerInventory = firstPersonPlayer.GetComponent<PlayerInventory>();

        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //if the user presses escaoe
        if (Input.GetKeyDown(KeyCode.Escape)) {
            //if the game is paused
            if (isPaused) {
                //unpause the game
                Cursor.lockState = CursorLockMode.Locked;
                pauseScreen.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
            else {
                //otherwise, pause the game
                Cursor.lockState = CursorLockMode.None;
                pauseScreen.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
        }
    }

    public void QuickSave() {

        //get the values to be saved
        playerPos = firstPersonPlayer.transform.position;
        playerRotation = firstPersonPlayer.transform.rotation;
        cameraRotation = playerCamera.transform.rotation;
        knifeCount = playerInventory.knifeCount;
        objectiveComplete = playerInventory.objectiveComplete;

        //assign them to the instance
        Instance = this;

        //get the save path
        string savePath = Application.persistentDataPath + "save.json";

        //convert the instance to JSON and write it to the save path
        string json = JsonUtility.ToJson(Instance);
        File.WriteAllText(savePath, json);

        //unpause the game
        Cursor.lockState = CursorLockMode.Locked;
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void QuickLoad() {

        //get the save path
        string savePath = Application.persistentDataPath + "save.json";

        //if the save file exists
        if (File.Exists(savePath)) {
            string json = File.ReadAllText(savePath);       //read the file
            JsonUtility.FromJsonOverwrite(json, Instance);  //read the json string and create an instance variable with it
        }
        else {
            Debug.LogError("Unable to load from file: " + savePath);  //display a debug message
        }

        //get the character controller
        CharacterController playerController = firstPersonPlayer.GetComponent<CharacterController>();

        //disable the character controller, move the character's positon, re-enable the character controller
        playerController.enabled = false;
        firstPersonPlayer.transform.localPosition = playerPos;
        playerController.enabled = true;

        //set the player's rotation
        firstPersonPlayer.transform.rotation = playerRotation;

        //set the camera's rotation
        playerCamera.transform.rotation = cameraRotation;

        //set the inventory count
        playerInventory.knifeCount = Instance.knifeCount;
        playerInventory.objectiveComplete = Instance.objectiveComplete;

        //unpause the game
        Cursor.lockState = CursorLockMode.Locked;
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
