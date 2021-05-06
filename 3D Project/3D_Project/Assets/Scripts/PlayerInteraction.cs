using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float range = 4f;

    [SerializeField] private Text interactionText;
    [SerializeField] private LayerMask interactableLayer;

    private void Update() {
        RaycastHit hit;
        InteractableObject interactableObject = null;

        //if the player is looking at an interactable object
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, range, interactableLayer)) {
            interactableObject = hit.collider.gameObject.GetComponent<InteractableObject>();

            if (interactableObject != null) {
                interactionText.text = interactableObject.GetInteractionText();
            }
            else {
                interactionText.text = "";
            }
        }
        else {
            interactionText.text = "";
        }

        if (Input.GetButtonDown("Fire2") && interactableObject != null) {
            interactableObject.Activate();
        }
    }
}
