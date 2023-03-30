using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int MaxHealth = 10;
    public int MoveSpeed = 5;


    private int currentHealth;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Debug.Log("Enemy is dead!");
            isDead = true;
        }
        
    }

    public void applyDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            this.die();
        }
    }

    public void die()
    {
        isDead = true;
        //start death sequence?
        gameObject.SetActive(false);
    }

}
