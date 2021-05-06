using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    [SerializeField] Transform[] waypoints;
    [SerializeField] float closeEnoughDistance = 0.25f;
    [SerializeField] bool loop = false;

    private NavMeshAgent agent = null;
    private Animator animator = null;

    private int wayPointIndex = 0;
    public bool startWalking = false;
    private bool patrolling = true;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!patrolling) {
            return;
        }

        if (startWalking) {
            agent.SetDestination(waypoints[wayPointIndex].position);
            startWalking = false;
        }

        float distanceToTarget = Vector3.Distance(agent.transform.position, waypoints[wayPointIndex].position);
        if (distanceToTarget < closeEnoughDistance) {

            wayPointIndex++;

            if (wayPointIndex >= waypoints.Length) {
                if (loop) {
                    wayPointIndex = 0;
                }
                else {
                    patrolling = false;
                    animator.SetFloat("Speed", 0);
                    return;
                }
            }
            agent.SetDestination(waypoints[wayPointIndex].position);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
