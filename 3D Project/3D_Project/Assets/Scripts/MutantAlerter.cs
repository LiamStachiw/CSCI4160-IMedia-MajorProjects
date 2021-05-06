using UnityEngine;
using UnityEngine.AI;

public class MutantAlerter : MonoBehaviour {
    [SerializeField] GameObject deathScreen;

    private GameObject mutant = null;
    private NavMeshAgent nm = null;
    private MutantMovement movement = null;
    private Vector3 target = Vector3.zero;

    public bool playerIsDead = false;
    public bool playerIsVisible = false;
    public float distanceToTarget;

    void Update() {

        //if the mutant has spotted the player
        if (mutant != null && nm != null && movement != null && target != Vector3.zero) {

            //get the distance to the current targer
            distanceToTarget = Vector3.Distance(nm.transform.position, target);

            //if the mutant is close to the player and the player is visible
            if (distanceToTarget < 4f && playerIsVisible) {

                //end the game, the player is dead
                Debug.Log("You are dead");
                playerIsDead = true;

                Cursor.lockState = CursorLockMode.None;
                deathScreen.SetActive(true);

            }
            //if they are close enough but the player is NOT visible
            else if (distanceToTarget < 4f && !playerIsVisible) {

                //the player has escaped, randomly set a new target and start the cycle over again

                Debug.Log("Player Escaped");

                nm.speed = 3f;

                movement.seesPlayer = false;

                if (!movement.randomWander) {

                    nm.SetDestination(movement.waypoints[0].position);
                    movement.waypointIndex = 0;

                }
                else {

                    target = new Vector3(mutant.transform.position.x + Random.Range(-movement.distanceToNextTarget, movement.distanceToNextTarget),
                                         mutant.transform.position.y,
                                         mutant.transform.position.z + Random.Range(-movement.distanceToNextTarget, movement.distanceToNextTarget));

                    while (!Physics.Raycast(target, Vector3.down, 1.5f, movement.groundLayer)) {
                        if (Physics.Raycast(target, Vector3.down, 100f, movement.groundLayer)) {
                            target.y -= 1f;
                        }
                        else {
                            target.y += 1f;
                        }
                    }
                }
            }
        }
    }

    //if the player is in the mutant's vision cone and is not dead
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Mutant") && !playerIsDead) {

            //set the necessary objects
            mutant = other.gameObject;
            nm = other.GetComponentInParent<NavMeshAgent>();
            movement = other.GetComponentInParent<MutantMovement>();

            //set the player's position as the mutant's target
            target = transform.position;

            //set the mutant to run at the target
            nm.speed = 10f;
            nm.SetDestination(target);

            playerIsVisible = true;
            movement.seesPlayer = true;
        }
    }

    //if the player leaves the mutant's vision cone
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Mutant")) {
            playerIsVisible = false;
        }
    }
}
