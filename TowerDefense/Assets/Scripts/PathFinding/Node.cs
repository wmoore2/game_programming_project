using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Dist { get; set; } = int.MaxValue;
    public Node Prev { get; set; } = null;
    public int X;
    public int Z;

    public void Reset()
    {
        Dist = int.MaxValue;
        Prev = null;
    }
}
