using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateRandomMove : IState
{
    private static readonly System.Random random = new System.Random();
    private static readonly (int x, int z)[] directions = new (int x, int z)[]
    {
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    };

    private (int x, int z) randomDirection;

    private IState _nextState;

    public StateRandomMove()
    {
        randomDirection = directions[random.Next() % 4];
        _nextState = this;
    }
    public bool Execute(EnemyController enemy)
    {
        var grid = GameObject.FindObjectOfType<GridController>();
        var currCoord = grid.positionToGridCoordinates(enemy.transform.position);
        var targetCoord = (currCoord.Item1 + randomDirection.x, currCoord.Item2 + randomDirection.z);
        if (!grid.PositionIsInGrid(targetCoord) || !grid.isSpotFree(targetCoord.Item1, targetCoord.Item2)){
            _nextState = new StateExplode();
            return true;
        }
        else
        {
            enemy.Move(grid.gridCoordinatesToPosition(targetCoord.Item1, targetCoord.Item2));
            return false;
        }
    }

    public IState NextState()
    {
        return _nextState;
    }
}
