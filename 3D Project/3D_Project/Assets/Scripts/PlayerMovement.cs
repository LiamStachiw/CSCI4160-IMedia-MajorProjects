using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private Transform footPosition;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    private Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update() {
        //check if the player is grounded
        isGrounded = Physics.CheckSphere(footPosition.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0) {
            velocity.y = gravity;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //calculate the move vector
        Vector3 move = transform.right * x + transform.forward * z;

        //move the player
        controller.Move(move * speed * Time.deltaTime);

        //if the player wasnts to jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            //add a jumping force
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //add gravity
        velocity.y += gravity * Time.deltaTime;

        //apply gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
