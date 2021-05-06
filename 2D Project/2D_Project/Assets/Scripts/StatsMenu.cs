using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour {
    [Header("Upgrade Amounts")]
    [SerializeField] private float accelerationUpgrade = 0.0002f;
    [SerializeField] private float turnSpeedUpgrade = 10f;
    [SerializeField] private float maxSpeedUpgrade = 1f;
    [SerializeField] private int firepowerUpgrade = 1;
    [Header("Base Upgrade Costs")]
    [SerializeField] private int accelerationCost = 50;
    [SerializeField] private int turnSpeedCost = 25;
    [SerializeField] private int maxSpeedCost = 75;
    [SerializeField] private int firepowerCost = 100;
    [Header("Maximum Levels")]
    [SerializeField] private int maxAccelerationLevel = 10;
    [SerializeField] private int maxMaxSpeedLevel = 10;
    [SerializeField] private int maxTurnSpeedLevel = 10;
    [SerializeField] private int maxFirepowerLevel = 1000;


    private GameObject confirmationDialog;
    private Text creditsVal;
    private GameObject[] levels;
    private GameObject[] costs;
    private AudioSource purchase;

    private void Awake() {
        //get the required components
        confirmationDialog = GameObject.FindGameObjectWithTag("Confirmation");
        confirmationDialog.transform.localScale = new Vector3(0, 0, 0);                 //set the scale of the confirmation dialog to be 0 (ie. disabled)
        creditsVal = GameObject.FindGameObjectWithTag("Credits").GetComponent<Text>();
        levels = GameObject.FindGameObjectsWithTag("Level");
        costs = GameObject.FindGameObjectsWithTag("Cost");
        purchase = gameObject.GetComponent<AudioSource>();
    }

    private void Update() {
        //Update the necessary components
        creditsVal.text = PlayerStats.instance.availableCredits.ToString();
        UpdateLevels();
        UpdateCosts();
    }

    public void StartGame() {
        //load the game scene
        SceneManager.LoadScene("Game");
    }

    public void UpgradeAcceleration(Text lvlText) {
        //if the acceleration is not already the max level and the player has enough credits to purchase an upgrade
        if (int.Parse(lvlText.text) != maxAccelerationLevel && (PlayerStats.instance.availableCredits >= CalculateCost(int.Parse(lvlText.text), "Acceleration"))) {
            PlayerStats.instance.availableCredits -= CalculateCost(int.Parse(lvlText.text), "Acceleration");    //subtract the cost from the player's avilable credits
            PlayerStats.instance.accelerationLevel += 1;                                                        //increase the level by 1
            lvlText.text = PlayerStats.instance.accelerationLevel.ToString();                                   //update the text of the displayed level
            PlayerStats.instance.acceleration = accelerationUpgrade * PlayerStats.instance.accelerationLevel;   //update the player's stat
            purchase.Play();                                                                                    //play the purchase audio
        }
    }

    public void UpgradeMaxSpeed(Text lvlText) {
        //if the max speed is not already the max level and the player has enough credits to purchase an upgrade
        if (int.Parse(lvlText.text) != maxMaxSpeedLevel && (PlayerStats.instance.availableCredits >= CalculateCost(int.Parse(lvlText.text), "MaxSpeed"))) {
            PlayerStats.instance.availableCredits -= CalculateCost(int.Parse(lvlText.text), "MaxSpeed");    //subtract the cost from the player's avilable credits
            PlayerStats.instance.maxSpeedLevel += 1;                                                        //increase the level by 1
            lvlText.text = PlayerStats.instance.maxSpeedLevel.ToString();                                   //update the text of the displayed level
            PlayerStats.instance.maxSpeed = maxSpeedUpgrade * PlayerStats.instance.maxSpeedLevel;           //update the player's stat
            purchase.Play();                                                                                //play the purchase audio
        }
    }

    public void UpgradeTurnSpeed(Text lvlText) {
        //if the turn speed is not already the max level and the player has enough credits to purchase an upgrade
        if (int.Parse(lvlText.text) != maxTurnSpeedLevel && (PlayerStats.instance.availableCredits >= CalculateCost(int.Parse(lvlText.text), "TurnSpeed"))) {
            PlayerStats.instance.availableCredits -= CalculateCost(int.Parse(lvlText.text), "TurnSpeed");   //subtract the cost from the player's avilable credits
            PlayerStats.instance.turnSpeedLevel += 1;                                                       //increase the level by 1
            lvlText.text = PlayerStats.instance.turnSpeedLevel.ToString();                                  //update the text of the displayed level
            PlayerStats.instance.turnSpeed = turnSpeedUpgrade * PlayerStats.instance.turnSpeedLevel;        //update the player's stat
            purchase.Play();                                                                                //play the purchase audio
        }
    }

    public void UpgradeFirepower(Text lvlText) {
        //if the firepower is not already the max level and the player has enough credits to purchase an upgrade
        if (int.Parse(lvlText.text) != maxFirepowerLevel && (PlayerStats.instance.availableCredits >= CalculateCost(int.Parse(lvlText.text), "Firepower"))) {
            PlayerStats.instance.availableCredits -= CalculateCost(int.Parse(lvlText.text), "Firepower");   //subtract the cost from the player's avilable credits
            PlayerStats.instance.firepowerLevel += 1;                                                       //increase the level by 1
            lvlText.text = PlayerStats.instance.firepowerLevel.ToString();                                  //update the text of the displayed level
            PlayerStats.instance.firePower = PlayerStats.instance.firepowerLevel * firepowerUpgrade;        //update the player's stat
            purchase.Play();                                                                                //play the purchase audio
        }
    }

    private int CalculateCost(int currentLevel, string stat) {
        if (stat == "Acceleration") {
            return currentLevel * accelerationCost;
        }
        else if (stat == "MaxSpeed") {
            return currentLevel * maxSpeedCost;
        }
        else if (stat == "TurnSpeed") {
            return currentLevel * turnSpeedCost;
        }
        else if (stat == "Firepower") {
            return currentLevel * firepowerCost;
        }
        else {
            return 0;
        }
    }

    //Update the displayed level of each stat
    private void UpdateLevels() {
        if (levels != null) {
            levels[0].GetComponent<Text>().text = PlayerStats.instance.accelerationLevel.ToString();
            levels[1].GetComponent<Text>().text = PlayerStats.instance.maxSpeedLevel.ToString();
            levels[2].GetComponent<Text>().text = PlayerStats.instance.turnSpeedLevel.ToString();
            levels[3].GetComponent<Text>().text = PlayerStats.instance.firepowerLevel.ToString();
        }
    }

    //Update the displayed cost of each stat upgrade
    private void UpdateCosts() {
        if (costs != null) {
            if (PlayerStats.instance.accelerationLevel == maxAccelerationLevel) {
                costs[0].GetComponent<Text>().text = "Max Level";
            }
            else {
                costs[0].GetComponent<Text>().text = (PlayerStats.instance.accelerationLevel * accelerationCost).ToString();
            }
            if (PlayerStats.instance.maxSpeedLevel == maxMaxSpeedLevel) {
                costs[1].GetComponent<Text>().text = "Max Level";
            }
            else {
                costs[1].GetComponent<Text>().text = (PlayerStats.instance.maxSpeedLevel * maxSpeedCost).ToString();
            }
            if (PlayerStats.instance.turnSpeedLevel == maxTurnSpeedLevel) {
                costs[2].GetComponent<Text>().text = "Max Level";
            }
            else {
                costs[2].GetComponent<Text>().text = (PlayerStats.instance.turnSpeedLevel * turnSpeedCost).ToString();
            }
            if (PlayerStats.instance.firepowerLevel == maxFirepowerLevel) {
                costs[3].GetComponent<Text>().text = "Max Level";
            }
            else {
                costs[3].GetComponent<Text>().text = (PlayerStats.instance.firepowerLevel * firepowerCost).ToString();
            }
        }
    }

    //Reset all of the player's stats
    public void ResetStats(bool selection) {
        if (selection) {
            PlayerStats.instance.availableCredits = 0;
            PlayerStats.instance.acceleration = 0.0001f;
            PlayerStats.instance.turnSpeed = 25f;
            PlayerStats.instance.maxSpeed = 1f;
            PlayerStats.instance.firePower = 1;
            PlayerStats.instance.accelerationLevel = 1;
            PlayerStats.instance.turnSpeedLevel = 1;
            PlayerStats.instance.maxSpeedLevel = 1;
            PlayerStats.instance.firepowerLevel = 1;
            PlayerStats.SaveStatsAsJSON();
        }

        //hide the confirmation dialog
        confirmationDialog.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ShowConfirmationBox() {
        //show the confirmation dialog
        confirmationDialog.transform.localScale = new Vector3(1, 1, 1);
    }
}
