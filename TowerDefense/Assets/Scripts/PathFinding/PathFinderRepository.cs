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
        var heartPathFinder = new PathFinder(GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>());
        pathFinders.Add(PathFinderType.HeartFinder, heartPathFinder);
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
