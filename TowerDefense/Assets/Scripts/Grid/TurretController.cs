using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float Damage = 2;
    //shots per second
    public float FireRate = 2;
    public float WeaponRange = 50;
    //radians per second to rotate
    public float RotationSpeed = 0.2f;

    public Transform BarrelEnd;

    //
    public Transform BarrelAnchor;

    //transform for vertical rotation
    public Transform BarrelRotate;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.08f);
    private LineRenderer laserLine;
    private float nextFire;
    private Runner runner;
    private List<GameObject> enemies;
    private bool hasTarget = false;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        runner = GameObject.FindWithTag("Runner").GetComponent<Runner>();

        //set forward to be the up direction
        BarrelRotate.forward = BarrelRotate.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAimedAtTarget())
        {
            Shoot();
        }
        aim();
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private bool isAimedAtTarget()
    {
        return true;
    }

    private GameObject GetClosestEnemy()
    {
        enemies = runner.Enemies.OrderBy(obj => (BarrelAnchor.position - obj.transform.position).sqrMagnitude).ToList();
        if (enemies.Count > 0)
        {
            return enemies[0];

        }
        return null;
    }

    private void aim()
    {
        if (!hasTarget)
        {
            target = GetClosestEnemy();
            if (target != null)
            {
                hasTarget = true;
            }
        }

        if (target != null)
        {
            //aiming
            Vector3 turretTarget = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, RotationSpeed * Time.deltaTime, 0.0f);
            Vector3 barrelTarget = Vector3.RotateTowards(BarrelRotate.forward, target.transform.position - BarrelRotate.position, RotationSpeed * Time.deltaTime, 0.0f);

            turretTarget.y = 0;

            //actually rotate
            BarrelRotate.rotation = Quaternion.LookRotation(barrelTarget);
            transform.rotation = Quaternion.LookRotation(turretTarget);

        } else
        {
            hasTarget = false;
        }
        //need to figure out what angle each transform should be rotated to
        //1. find closest enemy
        //2. find horizontal rotation and vertical rotation
        //3. rotate both simultaneously


    }

    private void Shoot()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Time.time > nextFire)
        {
            // Update the time when our player can fire next
            nextFire = Time.time + 1 / FireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = BarrelEnd.position;

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, BarrelEnd.position);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, BarrelRotate.forward, out hit, WeaponRange))
            {
                Debug.Log("Hit something");
                // Set the end position for our laser line 
                laserLine.SetPosition(1, hit.point);

                // Get a reference to a health script attached to the collider we hit
                EnemyStatus health = hit.collider.GetComponent<EnemyStatus>();

                // If there was a health script attached
                if (health != null)
                {
                    Debug.Log("Took health");
                    // Call the damage function of that script, passing in our gunDamage variable
                    health.applyDamage(Damage);
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (BarrelEnd.forward * WeaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        //gunAudio.Play();

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
}
