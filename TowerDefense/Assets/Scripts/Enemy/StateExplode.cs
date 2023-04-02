using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateExplode : State
{
    public override void Execute(EnemyController character)
    {
        character.Explode();
    }
}
