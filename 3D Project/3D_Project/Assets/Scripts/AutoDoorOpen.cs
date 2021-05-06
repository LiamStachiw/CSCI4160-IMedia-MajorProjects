using UnityEngine;

public class AutoDoorOpen : MonoBehaviour
{
    Animator anim = null;

    private void OnTriggerEnter(Collider other) {
        //if the player enters the door's trigger
        if (other.CompareTag("AutoDoor")) {
            anim = other.gameObject.GetComponent<Animator>();

            //set the door to open
            if(anim != null) {
                anim.SetBool("IsOpen", true);
            }

        }
    }

    private void OnTriggerExit(Collider other) {
        //if the player exits the door's trigger
        if (other.CompareTag("AutoDoor")) {
            anim = other.gameObject.GetComponent<Animator>();

            //set the door to close
            if (anim != null) {
                anim.SetBool("IsOpen", false);
            }

        }
    }
}
