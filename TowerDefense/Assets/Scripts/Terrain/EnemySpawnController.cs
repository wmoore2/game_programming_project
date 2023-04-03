using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject enemyNormal;
    public GameObject enemyElite;
    private Vector3 SpawnPos;
    private GridController grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindObjectOfType<GridController>();
        SpawnPos = grid.gridCoordinatesToPosition(0, 15) + new Vector3(0, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnNormal()
    {
        return Instantiate(enemyNormal, SpawnPos, Quaternion.identity);
    }

    public GameObject SpawnElite()
    {
        return Instantiate(enemyElite, SpawnPos, Quaternion.identity);
    }
}
