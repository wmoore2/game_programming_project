using UnityEngine;
using System.Collections;

public abstract class State
{
	public abstract void Execute(EnemyController character);
}