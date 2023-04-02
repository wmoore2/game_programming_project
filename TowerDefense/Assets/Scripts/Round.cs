using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    //A round is basically an instruction set for the runner
    public int StartingRound = 1;
    public int MaxRounds = 10;
    public int BaseEnemies = 5;
    public int EliteRatio = 5;
    public bool isGameOver = false;

    //affects the spawn rate and other things
    public float ScalingFactor = 1.05f;

    private int roundNumber;
    private List<Runner.EnemyType> toSpawn = new List<Runner.EnemyType>(); 

    // Start is called before the first frame update
    void Start()
    {
        roundNumber = StartingRound;
        generateNewRound();
    }

    public bool IsSpawnQueueEmpty()
    {
        return toSpawn.Count == 0;
    }

    private void generateNewRound()
    {
        toSpawn.Clear();
        toSpawn.TrimExcess();

        //Adds baseEnemies to the number of enemies added last round, then adds it to the total enemies last round
        int numEnemies = (BaseEnemies / 2) * (roundNumber * (roundNumber - 1));
        for (int i = 1; i <= numEnemies; i++)
        {
            toSpawn.Add(Runner.EnemyType.Normal);
            if (i % EliteRatio == 0)
            {
                toSpawn.Add(Runner.EnemyType.Elite);
            }
        }
    }

    public void AdvanceRound()
    {
        roundNumber++;
        if (roundNumber > MaxRounds)
        {
            isGameOver = true;
        }
        generateNewRound();
    }

    public Runner.EnemyType NextEnemy()
    {
        Runner.EnemyType toReturn = toSpawn[0];
        toSpawn.RemoveAt(0);
        toSpawn.TrimExcess();

        return toReturn;
    }
}
