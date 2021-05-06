using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AsteroidMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] private float minSpeed = 75f;
    [SerializeField] private float maxSpeed = 100f;
    [Header("Prefabs")]
    [SerializeField] public List<GameObject> fragments;
    [SerializeField] public GameObject fragmentSpawnLocation;
    [SerializeField] private GameObject asteroidPrefab;
    [Header("Fragmentation values")]
    [SerializeField] public int minFragments = 2;
    [SerializeField] public int maxFragments = 4;
    [SerializeField] public int creditsValue = 0;

    private float movementSpeed;
    private float rotation;
    private Vector3 movement;
    private Quaternion spin;
    private new AudioSource audio;
    private Rigidbody2D rb;
    private Text creditsVal;
    private GameObject[] spawnPositions;

    private void Awake() {
        //Get the required components
        creditsVal = GameObject.FindGameObjectWithTag("Credits").GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        
        //generate a random movement speed and rotation
        movementSpeed = Random.Range(minSpeed, maxSpeed);
        rotation = Random.Range(0f, 360f);

        //convert the rotation inro a Quaternion
        spin = Quaternion.Euler(Vector3.forward * rotation);

        //apply the rotation
        transform.rotation = spin;
        rb.rotation = rotation;

        //apply the movement speed
        movement = Vector3.up * movementSpeed;
        rb.AddRelativeForce(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //If the asteroid collides with a laser
        if (collision.CompareTag("Laser")) {

            //get the required components
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            PlayerMovement playerMovement = player[1].GetComponent<PlayerMovement>();
            AsteroidMovement parentAsteroid = gameObject.GetComponent<AsteroidMovement>();

            //play the explosion audio
            audio.Play();

            //reduce the projectile count by 1
            playerMovement.projectilesOnScreen -= 1;

            //add credits to the player's stats
            PlayerStats.instance.availableCredits += parentAsteroid.creditsValue;

            //update the credits amount in the HUD
            creditsVal.text = PlayerStats.instance.availableCredits.ToString();

            //destroy the laser GameObject
            Destroy(collision.gameObject);

            //disable the collision of the asteroid
            GetComponent<Collider2D>().enabled = false;

            //set the asteroid to play its explosion animation
            GetComponent<Animator>().SetBool("IsDestroyed", true);

            //get a random amount of fragments to create based on the given min and max values
            int numFragments = Random.Range(parentAsteroid.minFragments, parentAsteroid.maxFragments+1);


            for (int i = 0; i < numFragments; i++) {
                //generate a random size for the fragemnt
                float fragmentScale = Random.Range(0.4f, 0.6f);
                //select which fragment prefab to spawn
                int fragmentToAdd = Random.Range(0, parentAsteroid.fragments.Count - 1);
                //get the spawn location of the new fragment
                Vector3 fragmentPosition = parentAsteroid.fragmentSpawnLocation.transform.position;

                //instantiate a new fragment prefab
                GameObject newFragment = Instantiate(parentAsteroid.fragments[fragmentToAdd], fragmentPosition, parentAsteroid.transform.rotation);
                //adjust the scale of the new prefab
                newFragment.transform.localScale = new Vector3(fragmentScale, fragmentScale, fragmentScale);
            }

            //get the spawn positions within the game
            spawnPositions = GameObject.FindGameObjectsWithTag("Spawn Position");
            //select a random spawn position
            int randPos = Random.Range(0, spawnPositions.Length);
            //create a vector with the new spawn postion
            Vector3 posInCam = spawnPositions[randPos].transform.position;

            //if the laser collided with an asteroid (NOT a fragment)
            if (CompareTag("Asteroid")) {
                //re-enable collision of the original asteroid
                GetComponent<Collider2D>().enabled = true;
                //instantiate a new asteroid in the random spawn location
                _ = Instantiate(asteroidPrefab, posInCam, Quaternion.identity);
                //disable the collision of the original asteroid again
                GetComponent<Collider2D>().enabled = false;
            }
         
            //if the asteroid collides with a player
        } else if (collision.CompareTag("Player")) {
            collision.enabled = false;                                  //disable the player's collision
            collision.GetComponent<Animator>().SetBool("IsDead", true); //set the player to play its death animation
            collision.GetComponents<AudioSource>()[1].Stop();           //stop the thruster sound effect
            collision.GetComponents<AudioSource>()[2].Play();           //play the expolsion sound effect
        }
    }
}
