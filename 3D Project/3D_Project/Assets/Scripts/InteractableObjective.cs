using UnityEngine;

public class InteractableObjective : InteractableObject {

    [SerializeField] private string beforeText = "Right-click to take the jerry cans";
    [SerializeField] private string afterText = "You have collected the jerry cans";
    [SerializeField] private PlayerInventory inventory;

    private bool isCollected = false;

    public override void Activate() {
        if (!isCollected) {
            isCollected = true;
            inventory.objectiveComplete = true;
            activateText = afterText;

            gameObject.SetActive(false);

        }
        else {
            activateText = beforeText;
        }
    }

    public override string GetInteractionText() {
        if (isCollected) {
            return afterText;
        }
        else {
            return beforeText;
        }
    }
}
