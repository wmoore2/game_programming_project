using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathFinderType
{
    HeartFinder,
    PlayerFinder
}

public class PathFinderRepository : MonoBehaviour
{
    private Dictionary<PathFinderType, IPathFinder> pathFinders;

    void Awake()
    {
        pathFinders = new Dictionary<PathFinderType, IPathFinder>();
        var heartPathFinder = new PathFinder(GameObject.FindObjectOfType<GridController>());
        pathFinders.Add(PathFinderType.HeartFinder, heartPathFinder);

        var playerPathFinder = new PathFinder(GameObject.FindObjectOfType<PlayerWatcher>());
        pathFinders.Add(PathFinderType.PlayerFinder, playerPathFinder);
    }

    public IPathFinder GetPathFinder(PathFinderType type)
    {
        return pathFinders[type];
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
