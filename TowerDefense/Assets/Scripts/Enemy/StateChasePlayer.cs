using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChasePlayer : IState
{
    IState _nextState;

    public bool Execute(EnemyController enemy)
    {
        if (enemy.IsDead)
        {
            enemy.Die();
            _nextState = new StateNull();
            return true;
        }
        IPathFinder pfinder = GameObject.FindObjectOfType<PathFinderRepository>().GetPathFinder(PathFinderType.PlayerFinder);
        var target = pfinder.NextStep(enemy.transform.position);
        if (target is not null)
        {
            enemy.Move(target.Value);
            _nextState = this;
            return false;
        }
        else
        {
            _nextState = new StateExplode();
            return true;
        }
    }

    public IState NextState()
    {
        return _nextState;
    }
}
