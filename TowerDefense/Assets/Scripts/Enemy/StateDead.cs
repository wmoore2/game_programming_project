using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : State
{
    public override void Execute(EnemyController character)
    {
        character.Die();
    }
}
