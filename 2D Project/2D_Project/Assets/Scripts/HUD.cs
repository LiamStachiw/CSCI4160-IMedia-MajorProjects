using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Text creditsVal;

    // Start is called before the first frame update
    void Start()
    {
        //get the Text object in the HUD
        creditsVal = GameObject.FindGameObjectWithTag("Credits").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //update the HUD text to display the correct value
        creditsVal.text = PlayerStats.instance.availableCredits.ToString();
    }
}
