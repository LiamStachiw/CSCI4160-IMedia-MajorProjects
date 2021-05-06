using UnityEngine;
using UnityEngine.AI;

public class MutantDamageRagdoll : MonoBehaviour {

    public Ragdoller ragdoll = null;

    private NavMeshAgent agent = null;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Death() {

        //if there is a ragdoll
        if (ragdoll != null) {
            //set the ragdoll to active, set the mutant to inactive and ragdoll the ragdoll
            ragdoll.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
            ragdoll.Ragdoll(transform);
        }

        //if there is an agent, disable it
        if (agent != null) {
            agent.enabled = false;
        }
    }
}
