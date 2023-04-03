using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartStatus : MonoBehaviour
{
    public float MaxHealth = 100.0f;


    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    private bool isDead = false;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void applyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0 && !isDead)
        {
            isDead = true;
            Debug.Log("dead");
            //game is over here? not sure what do
        }
    }
}
