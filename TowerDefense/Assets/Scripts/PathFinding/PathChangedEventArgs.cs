using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathChangedEventArgs : EventArgs
{
    public int targetX { get; private set; }
    public int targetZ { get; private set; }


    public PathChangedEventArgs(int tx, int tz)
    {
        targetX = tx;
        targetZ = tz;
    }
}
