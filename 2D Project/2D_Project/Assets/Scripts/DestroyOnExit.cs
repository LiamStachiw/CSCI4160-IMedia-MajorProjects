using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // This function is used to destroy a GameObject after its animation has finished playing
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Destroy(animator.gameObject.transform.root.gameObject, stateInfo.length);
    }
}
