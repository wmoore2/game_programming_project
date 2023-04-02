using UnityEngine;
using System.Collections;

//A lot of this code was retrieved from an online unity tutorial found here: https://learn.unity.com/tutorial/let-s-try-shooting-with-raycasts

public class GunController : MonoBehaviour
{

    public float gunDamage = 1.0f;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                     // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

    public float hitmarkerDelay = 0.07f;

    private Camera fpsCam;                                                // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.02f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    //private AudioSource gunAudio;                                        // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing
    private GameObject hitmarker;

    void Start()
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();

        // Get and store a reference to our Camera by searching this GameObject and its parents
        fpsCam = GetComponentInParent<Camera>();

        hitmarker = GameObject.FindGameObjectWithTag("Hitmarker");
        hitmarker.SetActive(false);
    }

    public void hideGun()
    {
        gameObject.SetActive(false);
    }

    public void showGun()
    {
        gameObject.SetActive(true);
    }


    public void Shoot()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Time.time > nextFire)
        {
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, gunEnd.position);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);

                // Get a reference to a health script attached to the collider we hit
                EnemyStatus health = hit.collider.GetComponent<EnemyStatus>();

                // If there was a health script attached
                if (health != null)
                {
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.applyDamage(gunDamage);
                    //show hitmarker
                    StartCoroutine(ShowHitMarker());
                }

                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShowHitMarker()
    {
        hitmarker.SetActive(true);
        yield return new WaitForSeconds(hitmarkerDelay);
        hitmarker.SetActive(false);
    }

    private IEnumerator ShotEffect()
    {
        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
}
