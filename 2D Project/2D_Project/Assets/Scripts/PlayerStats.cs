using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerStats : MonoBehaviour
{
    //create a static instance variable
    public static PlayerStats instance { get; private set; }

    public int availableCredits;
    public float acceleration;
    public float turnSpeed;
    public float maxSpeed;
    public int firePower;

    public int accelerationLevel;
    public int turnSpeedLevel;
    public int maxSpeedLevel;
    public int firepowerLevel;

    private void Awake() {
        //if the instance is not already set
        if (instance == null) {
            
            //set all default values of the instance
            availableCredits = 0;
            acceleration = 0.0001f;
            turnSpeed = 25f;
            maxSpeed = 1f;
            firePower = 1;
            accelerationLevel = 1;
            turnSpeedLevel = 1;
            maxSpeedLevel = 1;
            firepowerLevel = 1;

            //set the instance
            instance = this;

            //if the game is unable to load the stats (ie. file does not already exist)
            if (!LoadStatsFromJson()) {
                //save the stats for loading in the future
                SaveStatsAsJSON();
            }
                    
        } else {
            Destroy(gameObject);
        }
        //Allow the stats manager to be available in every scene
        DontDestroyOnLoad(gameObject);
    }


    public static void SaveStatsAsJSON() {

        //create a filepath to save the player's stats to
        string saveString = Application.persistentDataPath + "savefile.json";

        //create a json string of the stats to save
        string json = JsonUtility.ToJson(instance);

        //save the stats to the filepath
        File.WriteAllText(saveString, json);
    }

    public static bool LoadStatsFromJson() {

        //create a filepath to load the player's stats from
        string saveString = Application.persistentDataPath + "savefile.json";

        //if the file exists
        if (File.Exists(saveString)) {
            string json = File.ReadAllText(saveString);     //read the file
            JsonUtility.FromJsonOverwrite(json, instance);  //read the json string and create an instance variable with it
            return true;                                    //return true (ie. file succussfully read)
        } else {
            Debug.LogError("Unable to load from file: " + saveString);  //display a debug message
            return false;                                               //return false (ie. file not successfully read)
        }
    }
}
