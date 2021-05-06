using UnityEngine;

public class Ragdoller : MonoBehaviour {

    [SerializeField] Rigidbody mainBody = null;

    public void Ragdoll(Transform newTransform) {

        // get all the transforms of the mutant and the ragdoll
        Transform[] allNewTransforms = newTransform.gameObject.GetComponentsInChildren<Transform>();
        Transform[] allThisTransforms = gameObject.GetComponentsInChildren<Transform>();

        //set the transforms of the ragdoll to be the same as the mutant
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;

        for (int i = 0; i < allNewTransforms.Length; i++) {
            allThisTransforms[i].position = allNewTransforms[i].position;
            allThisTransforms[i].rotation = allNewTransforms[i].rotation;
        }
    }
}
