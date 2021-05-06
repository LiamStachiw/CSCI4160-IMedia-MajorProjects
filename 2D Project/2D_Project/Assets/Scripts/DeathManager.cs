using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text pauseText;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null) {
            pauseText.text = "You have died.";
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
