using UnityEngine;

public class InteractableGenerator : InteractableObject {

    [SerializeField] private string noGasText = "You need to find some gas soon!";
    [SerializeField] private string beforeText = "Congratulations! You'll live to see another day!";
    [SerializeField] private string afterText = "Congratulations! You'll live to see another day!";
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject alarmLights;
    [SerializeField] private GameObject standardLights;

    private bool isFilled = false;

    public override void Activate() {
        if (inventory.objectiveComplete) {
            isFilled = true;
            alarmLights.SetActive(false);
            standardLights.SetActive(true);
        }
    }

    public override string GetInteractionText() {
        if (!inventory.objectiveComplete) {
            return noGasText;
        }
        else if (!isFilled) {
            return beforeText;
        }
        else {
            return afterText;
        }
    }
}
