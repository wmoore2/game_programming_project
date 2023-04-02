using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float MaxHealth = 10.0f;
    public float RespawnTimer = 15.0f;

    private float currentHealth;

    private bool isDead = false;
    private float _respawnDelta;
    private bool isRespawning = false;

    private Vector3 spawnLocation;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public bool IsRespawning
    {
        get { return isRespawning; }
        set { isRespawning = value; }
    }

    public enum rTypes
    {
        Chassis = 0,
        Jets,
        Optics
    };

    //array to store number of each resource player has
    public int[] resources = new int[3] {0, 0, 0};

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        isDead = false;
        resources[(int)rTypes.Chassis] = 50;
        spawnLocation = new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    public void Update()
    {
        if (isDead)
        {
            beDead();
        }

    }

    public void respawn()
    {
        isRespawning = false;
        isDead = false;
        _respawnDelta = 0;
        currentHealth = MaxHealth;
        transform.position = spawnLocation;
        gameObject.SetActive(true);
    }

    private void beDead()
    {
        if (isRespawning)
        {
            if (Time.time > _respawnDelta)
            {
                respawn();
            }
        }
    }

    public void pickupDrop(rTypes dropType, int amount)
    {
        Debug.Log("Player picked up " + amount + " " + dropType);
        resources[(int)dropType] += amount;
    }

    //returns true if resource was spent and false if there was insufficient resources
    public bool spendResource(rTypes dropType, int amount)
    {
        if (resources[(int)dropType] >= amount)
        {
            resources[(int)dropType] -= amount;
            return true;
        }

        return false;
    }

    public bool spendResources(rTypes dropType1, rTypes dropType2, int amount)
    {
        if (resources[(int)dropType1] >= amount && resources[(int)dropType2] >= amount)
        {
            resources[(int)dropType1] -= amount;
            resources[(int)dropType2] -= amount;
            return true;
        }

        return false;
    }

    public void applyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0 && !isDead)
        {
            this.die();
        }
    }

    public void die()
    {
        isDead = true;
        isRespawning = true;
        _respawnDelta = Time.time + RespawnTimer;
        gameObject.SetActive(false);
    }
}
