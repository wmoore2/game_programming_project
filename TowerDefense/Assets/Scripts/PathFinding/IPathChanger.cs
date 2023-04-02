using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPathChanger
{
    public event EventHandler<PathChangedEventArgs> PathChanged;
}
