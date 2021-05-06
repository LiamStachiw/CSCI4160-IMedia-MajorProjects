using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
    [SerializeField] public float knifeCount = 0;
    [SerializeField] public bool objectiveComplete = false;
    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Text knifeCountText;


    private void Update() {

        //update the HUD
        knifeCountText.text = "Knives left: " + knifeCount;

        //if the player is out of knives
        if (knifeCount == 0) {
            armsAnimator.SetBool("HasKnives", false);
        }
        else {
            armsAnimator.SetBool("HasKnives", true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //if the player walks over a knife
        if (other.CompareTag("Weapon")) {
            //destroy the knife and add 1 to the inventory count
            Destroy(other.gameObject);
            knifeCount += 1;
        }
    }
}
