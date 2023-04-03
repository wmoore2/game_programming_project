using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateExplode : IState
{
    public bool Execute(EnemyController enemy)
    {
        enemy.Explode();
        GameObject.FindObjectOfType<GridController>().DamageNearbyObjects(enemy.transform.position);
        return false;
    }

    public IState NextState()
    {
        return new StateNull();
    }
}
