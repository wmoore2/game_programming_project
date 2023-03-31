using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStatus : MonoBehaviour
{
    public float MaxHealth = 10.0f;

    private float currentHealth;

    private GridController grid;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;

        if(grid == null)
        {
            grid = gameObject.GetComponent<GridController>();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        

    }

    public void applyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            destroy();
        }
    }

    public void destroy()
    {
        grid.removeObstacle(transform.position);
        Destroy(gameObject);
    }
}
