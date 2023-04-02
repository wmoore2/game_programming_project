using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public GameObject Chassis;
    public GameObject Optics;
    public GameObject Jets;

    [Header("Drop Weightings")]
    public int ChassisWeight = 1;
    public int OpticsWeight = 1;
    public int JetsWeight = 1;
    [Space(10)]
    [Header("Chassis Drop Settings")]
    public int ChassisDropMin = 1;
    public int ChassisDropMax = 10;
    [Space(10)]
    [Header("Optics Drop Settings")]
    public int OpticsDropMin = 1;
    public int OpticsDropMax = 10;
    [Space(10)]
    [Header("Jets Drop Settings")]
    public int JetsDropMin = 1;
    public int JetsDropMax = 10;
    [Space(10)]
    [Header("Game Loop Settings")]
    [Tooltip("Number of enemies to spawn each second")]
    public float SpawnRate = 1;
    [Tooltip("Delay between round in seconds")]
    public float RoundDelay = 10;

    private float spawnDelayTimeout = 0;
    private float roundDelayTimeout = 0;

    private int totalWeight;
    private float dropYPos = 1.0f;

    public List<GameObject> Enemies
    {
        get { return new List<GameObject>(enemies); }
    }

    private List<GameObject> enemies = new List<GameObject>();
    private EnemySpawnController spawnController;
    private Round round;

    public enum EnemyType
    {
        Normal,
        Elite
    }

    // Start is called before the first frame update
    void Start()
    {
        initRunner();
    }

    // Update is called once per frame
    void Update()
    {
        gameLoop();
    }

    private void gameLoop()
    {
        if (round.isGameOver)
        {
            Debug.Log("Game is over!!");
            //game is over
            //show win or loss screen or something?
        } else
        {
            if (round.IsSpawnQueueEmpty())
            {
                //out of enemies to spawn
                if (EnemiesLeft() == 0)
                {
                    //out of enemies to spawn and all enemies are dead
                    //Advance the round delay
                    roundDelayTimeout = Time.time + RoundDelay;

                    //go to next round and scale spawn rate
                    SpawnRate *= round.ScalingFactor;
                    round.AdvanceRound();
                }
            } else
            {
                //enemies still need to be spawned
                //we check if the next round has actually started too
                if (Time.time > spawnDelayTimeout && Time.time > roundDelayTimeout)
                {
                    //set delay for spawning next enemy to be in 1/spawnRate seconds which corresponds to spawnRate enemies per second
                    spawnDelayTimeout = Time.time + (1.0f / SpawnRate);

                    //spawn enemy here
                    newEnemy(round.NextEnemy());
                }
            }
        }
    }

    private void newEnemy(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                newNormalEnemy();
                break;
            case EnemyType.Elite:
                newEliteEnemy();
                break;
        }
    }

    private void newNormalEnemy()
    {
        enemies.Add(spawnController.SpawnNormal());
    }

    private void newEliteEnemy()
    {
        enemies.Add(spawnController.SpawnElite());
    }

    private void initRunner()
    {
        calcTotalWeight();
        spawnDelayTimeout = 0;
        roundDelayTimeout = 0;
        Physics.IgnoreLayerCollision(7, 7);
        spawnController = GameObject.FindWithTag("EnemySpawn").GetComponent<EnemySpawnController>();
        round = gameObject.GetComponent<Round>();
    }

    public int EnemiesLeft()
    {
        return enemies.Count;
    }

    public void DeleteEnemy(GameObject toDelete)
    {
        enemies.Remove(toDelete);
    }

    private void calcTotalWeight()
    {
        totalWeight = ChassisWeight + OpticsWeight + JetsWeight;
    }

    public void CreateLoot(Vector3 position)
    {
        float drop = Random.Range(0, totalWeight);

        if (drop - (float)ChassisWeight < 0)
        {
            //Chassis drop
            createLoot(position, Chassis, ChassisDropMin, ChassisDropMax);
        } else if (drop - (float)(ChassisWeight + OpticsWeight) < 0)
        {
            //Optics drop
            createLoot(position, Optics, OpticsDropMin, OpticsDropMax);
        } else
        {
            //Jets drop
            createLoot(position, Jets, JetsDropMin, JetsDropMax);
        }
    }

    private void createLoot(Vector3 position, GameObject obj, int min, int max)
    {
        int amount = (int)Mathf.Floor(Random.Range(min, max + 1));
        position.y = dropYPos;
        LootController con = Instantiate(obj, position, Quaternion.identity).GetComponent<LootController>();
        con.setResourceAmount(amount);
    }
}
