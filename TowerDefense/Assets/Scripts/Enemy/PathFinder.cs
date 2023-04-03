using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PathFinder : IPathFinder
{
    private (int, int) _spawn;
    private (int, int) _target;
    private GridController _grid;
    private HashSet<(int, int)> _points;
    private Node[,] _graph;
    private static readonly (int, int)[] neighbourOffsets =
        new (int, int)[]{
            (-1, 0),
            (0, 1),
            (1, 0),
            (0, -1)
        };

    public PathFinder(params IPathChanger[] pathChangers)
    {
        _grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>();
        foreach (var pathChanger in pathChangers)
        {
            pathChanger.PathChanged += RecalculatePath;
        }
    }

    private void RecalculatePath(object sender, PathChangedEventArgs args)
    {
        int targetX = args.targetX;
        int targetZ = args.targetZ;

        var graph = new Node[_grid.Length * 2, _grid.Width * 2];
        var unvisited = new HashSet<Node>();

        foreach (var x in Enumerable.Range(0, _grid.Length * 2))
        {
            foreach (var z in Enumerable.Range(0, _grid.Width * 2))
            {
                if (_grid.isSpotFree(x, z))
                {
                    var node = new Node() { X = x, Z = z };
                    graph[x, z] = node;
                    unvisited.Add(node);
                }
                else
                {
                    graph[x, z] = null;
                }
            }
        }

        if (graph[targetX, targetZ] is not null)
        {
            // enemies stop *before* the destionation. Their explosion covers the 8 blocks around them.
            // Therefore treat target as always untraversable.
            unvisited.Remove(graph[targetX, targetZ]);
            graph[targetX, targetZ] = null;
        }

        foreach ((int neighbourX, int neighbourZ) in NeighbouringPositions(targetX, targetZ))
        {
            Node node = graph[neighbourX, neighbourZ];
            if (node is not null)
            {
                node.Dist = 0; // since the target's neighbours are the real destiontion nodes => dist = 0
            }
        }

        while (unvisited.Count > 0)
        {
            var currNode = unvisited.MinBy(n => n.Dist); // "Looks O(1) to me" ~ Dijkstra
            unvisited.Remove(currNode);
            if (currNode.Dist == int.MaxValue)
            {
                continue;
            }
            foreach ((int neighbourX, int neighbourZ) in NeighbouringPositions(currNode.X, currNode.Z))
            {
                Node node = graph[neighbourX, neighbourZ];
                if (node is not null && unvisited.Contains(node))
                {
                    node.Dist = currNode.Dist + 1;
                    node.Prev = currNode;
                }
            }
        }
        _graph = graph;
    }

    private IEnumerable<(int, int)> NeighbouringPositions(int x, int z)
    {
        foreach (var position in neighbourOffsets.Select( t => (x + t.Item1, z + t.Item2)))
        {
            if (_grid.PositionIsInGrid(position))
            {
                yield return position;
            }
        }
    }

    public Vector3? NextStep(Vector3 currentPosition)
    {
        (int x, int z) = _grid.positionToGridCoordinates(currentPosition);
        Node n = _graph[x, z];
        if (n.Dist == 0)
        {
            return null; // null when you're at your dest
        }
        n = n.Prev;
        return n is not null ? _grid.gridCoordinatesToPosition(n.X, n.Z) : null;
    }
}
