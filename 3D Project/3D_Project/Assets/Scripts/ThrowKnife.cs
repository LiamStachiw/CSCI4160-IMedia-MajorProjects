using UnityEngine;


public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private Animator armsAnimator;
    [SerializeField] private Transform knifeSpawnLocation;
    [SerializeField] private Rigidbody knifePrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private float throwSpeed = 25f;
    [SerializeField] private float rotationSpeed = 25f;

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            armsAnimator.SetTrigger("ThrowKnife");
        }
    }

    //This will be called using an animation event
    public void SpawnKnife() {
        Rigidbody newKnife = Instantiate(knifePrefab, knifeSpawnLocation.position, knifeSpawnLocation.rotation);
        newKnife.velocity = playerCamera.transform.forward * throwSpeed;
        newKnife.maxAngularVelocity = float.MaxValue;
        newKnife.AddRelativeTorque((Vector3.forward + Vector3.down) * rotationSpeed);

        inventory.knifeCount -= 1;
    }
}
