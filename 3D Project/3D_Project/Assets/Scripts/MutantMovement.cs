using UnityEngine;
using UnityEngine.AI;

public class MutantMovement : MonoBehaviour {

    [Header("Area Wander")]
    [SerializeField] public bool randomWander = false;
    [SerializeField] public float distanceToNextTarget = 10f;
    [SerializeField] public LayerMask groundLayer;

    [Header("Fixed Patrol")]
    [SerializeField] public Transform[] waypoints;
    [SerializeField] float idleTime = 1f;
    [SerializeField] bool loop = true;

    [Header("Other Values")]
    [SerializeField] float closeEnoughDistance = 4f;

    private NavMeshAgent agent = null;
    private Animator animator = null;

    private Vector3 nextTarget;

    public bool seesPlayer = false;
    public int waypointIndex = 0;
    private bool patrolling = true;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // set the next random targer
        nextTarget = new Vector3(transform.position.x + Random.Range(-distanceToNextTarget, distanceToNextTarget),
                                 transform.position.y,
                                 transform.position.z + Random.Range(-distanceToNextTarget, distanceToNextTarget));

        // while a short raycast from the new target does not hit the grounf
        while (!Physics.Raycast(nextTarget, Vector3.down, 1.5f, groundLayer)) {

            //check if the target is above the ground
            if (Physics.Raycast(nextTarget, Vector3.down, 100f, groundLayer)) {
                //if it is above the ground, move the y of the target down
                nextTarget.y -= 1f;
            }
            else {
                //if it is below the ground, move the y of the target up
                nextTarget.y += 1f;
            }
        }

        //if there is a NavMeshAgent and there are waypoints, set the first destination
        if ((agent != null) && (waypoints.Length > 0)) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    private void Update() {
        //if not patrolling, do nothing
        if (!patrolling) {
            return;
        }

        //if it is not set to randomly wander and it does not see the player
        if (!randomWander && !seesPlayer) {

            //get the distance to the waypoint
            float distanceToTarget = Vector3.Distance(agent.transform.position, waypoints[waypointIndex].position);

            //if it is close enough to the waypoint
            if (distanceToTarget < closeEnoughDistance) {
                waypointIndex++;

                if (waypointIndex >= waypoints.Length) {
                    if (loop) {
                        waypointIndex = 0;
                    }
                    else {
                        patrolling = false;
                        animator.SetFloat("Speed", 0);
                        return;
                    }
                }

                //set the destination to the next waypoint
                agent.SetDestination(waypoints[waypointIndex].position);
            }
        }
        // if it is set to randomly wander and it does not see the player
        else if (!seesPlayer) {
            //get the distance to the target
            float distanceToTarget = Vector3.Distance(agent.transform.position, nextTarget);

            //if it is close enough to the target
            if (distanceToTarget < closeEnoughDistance) {
                
                //randomly get a new target
                nextTarget = new Vector3(transform.position.x + Random.Range(-distanceToNextTarget, distanceToNextTarget),
                                         transform.position.y,
                                         transform.position.z + Random.Range(-distanceToNextTarget, distanceToNextTarget));

                // while a short raycast from the new target does not hit the grounf
                while (!Physics.Raycast(nextTarget, Vector3.down, 1.5f, groundLayer)) {

                    //check if the target is above the ground
                    if (Physics.Raycast(nextTarget, Vector3.down, 100f, groundLayer)) {
                        //if it is above the ground, move the y of the target down
                        nextTarget.y -= 1f;
                    }
                    else {
                        //if it is below the ground, move the y of the target up
                        nextTarget.y += 1f;
                    }
                }
            }

            //set the destination to the new target
            agent.SetDestination(nextTarget);

        }
        //set the speed of the animator to the magnitude of the velocity
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
