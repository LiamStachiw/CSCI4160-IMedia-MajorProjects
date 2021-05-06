using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StickInSurface : MonoBehaviour {
    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {

        //if the knife collides with a mutant
        if (collision.gameObject.CompareTag("Mutant")) {
            //get and call that mutant's death function
            MutantDamageRagdoll mdr = collision.gameObject.GetComponentInParent<MutantDamageRagdoll>();
            mdr.Death();
        }
        else {
            //stop the knife's velocity, set it to kinematic and set the parent to be what it collided with
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            gameObject.transform.parent = collision.transform;
        }
    }
}
