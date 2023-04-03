using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatcher : MonoBehaviour, IPathChanger
{
    GameObject _player;
    GridController _grid;
    private (int, int) _prevLoc;

    public event EventHandler<PathChangedEventArgs> PathChanged;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _grid = GameObject.FindObjectOfType<GridController>();
    }

    void Start()
    {
        _prevLoc = _grid.positionToGridCoordinates(this.transform.position);
        (int targetX, int targetZ) = _prevLoc;
        PathChanged.Invoke(this, new PathChangedEventArgs(targetX, targetZ));
    }

    // Update is called once per frame
    void Update()
    {
        var _curLoc = _grid.positionToGridCoordinates(this.transform.position);
        if (_curLoc != _prevLoc)
        {
            PathChanged?.Invoke(this, new PathChangedEventArgs(_curLoc.Item1, _curLoc.Item2));
        }
        _prevLoc = _curLoc;
    }
}
