using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private GameObject asteroidPrefab;

    private void Update() {
        //move the laser the correct amount
        Vector3 movement = Vector3.up * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        //if the laser goes off screen
        if (!GetComponent<Renderer>().isVisible) {
            //destroy the laster and decrease the count of projectiles on screen
            Destroy(this.gameObject);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            PlayerMovement playerMovement = player[1].GetComponent<PlayerMovement>();
            playerMovement.projectilesOnScreen -= 1;
        }
    }
}
