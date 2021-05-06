using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    //set the lock state for the main menu
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame() {
        //set the lock state and change the scene
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene("Game");
    }
}
