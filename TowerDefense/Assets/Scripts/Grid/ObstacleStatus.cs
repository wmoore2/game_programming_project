using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStatus : MonoBehaviour
{
    public float MaxHealth = 10.0f;

    private float currentHealth;

    private GridController grid;
    private TurretController turret;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;

        if(grid == null)
        {
            grid = gameObject.GetComponent<GridController>();
        }

        if(turret == null)
        {
            turret = gameObject.GetComponentInChildren<TurretController>();
        }

        HideTurret();
    }

    // Update is called once per frame
    public void Update()
    {
        

    }

    public void ShowTurret()
    {
        turret.Show();
    }

    public void HideTurret()
    {
        turret.Hide();
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
