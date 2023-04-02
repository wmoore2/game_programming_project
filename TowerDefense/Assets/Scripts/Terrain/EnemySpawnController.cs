using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject enemyNormal;
    public GameObject enemyElite;
    private Vector3 SpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPos = transform.position;
        SpawnPos.x += 3;

        //for testing
        SpawnPos = new Vector3(Random.Range(-40, 40), SpawnPos.y, Random.Range(-40, 40));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnNormal()
    {
        SpawnPos = new Vector3(Random.Range(-40, 40), SpawnPos.y, Random.Range(-40, 40));
        return Instantiate(enemyNormal, SpawnPos, Quaternion.identity);
    }

    public GameObject SpawnElite()
    {
        SpawnPos = new Vector3(Random.Range(-40, 40), SpawnPos.y, Random.Range(-40, 40));
        return Instantiate(enemyElite, SpawnPos, Quaternion.identity);
    }
}
