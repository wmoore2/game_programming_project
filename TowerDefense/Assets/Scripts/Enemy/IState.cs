using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public bool Execute(EnemyController enemy);

    public IState NextState();
}
