using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dummy state for when the enemy is dying
public class StateNull : IState
{
    public bool Execute(EnemyController enemy)
    {
        return false;
    }

    public IState NextState()
    {
        return this;
    }
}
