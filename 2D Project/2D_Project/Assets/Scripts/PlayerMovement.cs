using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private GameObject projectileSpawnPosition;
    [SerializeField] private GameObject projectile;

    [HideInInspector] public int projectilesOnScreen = 0;

    private float acceleration;
    private float turnSpeed;
    private float maxSpeed;
    private int maxProjectilesOnScreen;

    private Animator controller;
    private AudioSource laser;
    private AudioSource thruster;

    private Vector3 accelerate;

    private void Awake() {
        //get the required components
        controller = GetComponent<Animator>();
        laser = GetComponents<AudioSource>()[0];
        thruster = GetComponents<AudioSource>()[1];
    }

    private void Start() {
        //set the player's stats
        acceleration = PlayerStats.instance.acceleration;
        turnSpeed = PlayerStats.instance.turnSpeed;
        maxSpeed = PlayerStats.instance.maxSpeed;
        maxProjectilesOnScreen = PlayerStats.instance.firePower;
    }

    // Update is called once per frame
    void Update() {
        //determine which direction the user is trying to move
        float movement = Input.GetAxis("Vertical") * acceleration;
        float turning = -Input.GetAxis("Horizontal") * turnSpeed;
        Vector3 maxSpeedVec = Vector3.up * maxSpeed * Time.deltaTime;
        Vector3 maxReverseVec = Vector3.down * maxSpeed * Time.deltaTime;

        //add to the acceleration
        accelerate += Vector3.up * movement * Time.deltaTime;

        //if the player is moving above the max speed, limit the speed to be the max speed
        if (accelerate.magnitude > maxSpeedVec.magnitude) {
            if (accelerate.y < 0) {
                accelerate = maxReverseVec;
            }
            else {
                accelerate = maxSpeedVec;
            }
        }

        //move the player
        transform.Translate(accelerate);

        //turn the player in the direction they are trying to turn
        Vector3 spin = Vector3.forward * turning * Time.deltaTime;
        transform.Rotate(spin);

        //if the player is moving forward or backwards
        if (Input.GetAxis("Vertical") != 0) {
            //change the animation frame
            controller.SetBool("IsAccelerating", true);

            //play the thruster sound
            if (!thruster.isPlaying) {
                thruster.Play();
            }

        }
        else {
            //change the animation frame
            controller.SetBool("IsAccelerating", false);

            //stop the thruster sound
            thruster.Stop();
        }

        //If the player presses the Fire1(mouse1/ctrl) or jump(space) buttons
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) {

            //if there are below the max number of projectiles on the screen
            if (projectilesOnScreen < maxProjectilesOnScreen) {

                //create a laser prefab and increase the amount of projectiles on screen value
                Instantiate(projectile, projectileSpawnPosition.transform.position, projectileSpawnPosition.transform.rotation);
                projectilesOnScreen += 1;

                //play the laser sound effect
                laser.Play();
            }
        }
    }
}
