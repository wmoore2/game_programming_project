using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder
{
    Vector3? NextStep(Vector3 currentPosition);
}
