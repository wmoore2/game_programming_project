using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State
{
    public override void Execute(EnemyController character)
    {
        if (character.IsDead)
        {
            character.ChangeState(new StateDead());
        } else if (character.ShouldExplode())
        {
            character.ChangeState(new StateExplode());
        } else
        {
            character.Move();
        }
    }
}
