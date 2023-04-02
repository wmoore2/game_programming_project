using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float MaxHealth = 10.0f;
    private float currentHealth;

    private EnemyController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    public void Update()
    {
        

    }

    public void applyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0 && !controller.IsDead)
        {
            controller.IsDead = true;
        }
    }
}
