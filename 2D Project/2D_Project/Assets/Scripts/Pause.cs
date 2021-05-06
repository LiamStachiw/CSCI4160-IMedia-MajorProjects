using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    private bool isPaused = false;
    private GameObject pauseMenu;
    private AudioSource[] allAudio;

    // Start is called before the first frame update
    void Start() {
        //get the required components
        GameObject.FindGameObjectWithTag("HUD Text").GetComponent<Text>().text = "GAME PAUSED";
        allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        pauseMenu = GetComponentInChildren<Canvas>().gameObject;

        //diable the pause menu
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    // Update is called once per frame
    void Update() {
        //if the user presses the "escape" key
        if (Input.GetButtonDown("Cancel")) {

            //if the game is already paused
            if (isPaused) {
                //unpause each AudioSource that was playing when the game was paused
                foreach (AudioSource audioSource in allAudio) {
                    if (audioSource != null) {
                        audioSource.UnPause();
                    }
                }
                //disable the pause menu and resume the game
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            //if the game was not already paused
            else {
                //pause each AudioSource that might be playing
                foreach (AudioSource audioSource in allAudio) {
                    if (audioSource != null) {
                        audioSource.Pause();
                    }
                }
                //enable the pause menu and pause the game
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }

    public void EndRun() {
        SceneManager.LoadScene("StatsMenu");    //load the Stats Menu scene when the button is pressed
    }
}
