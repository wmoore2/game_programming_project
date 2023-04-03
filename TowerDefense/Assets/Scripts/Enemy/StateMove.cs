using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState
{
    public StateMove() { }
    private IState _nextState;
    public bool Execute(EnemyController enemy)
    {
        if (enemy.IsDead)
        {
            enemy.Die();
            _nextState = new StateNull();
            return true;
        }
        if (enemy.DamagedByPlayer)
        {
            _nextState = new StateChasePlayer();
            return true;
        }
        IPathFinder pfinder = GameObject.FindObjectOfType<PathFinderRepository>().GetPathFinder(PathFinderType.HeartFinder);
        var targetPos = pfinder.NextStep(enemy.transform.position);
        if (targetPos is not null)
        {
            enemy.Move(targetPos.Value);
            _nextState = this;
            return false;
        }
        else
        {
            _nextState = new StateRandomMove();
            return true;
        }
    }
    public IState NextState()
    {
        return _nextState;
    }
}
