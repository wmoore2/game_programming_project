using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int NumDrops = 1;
    private bool isDead;
    private bool hasExploded = false;
    private bool droppedLoot = false;
    private IState currentState;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    private bool _damagedByPlayer = false;
    public bool DamagedByPlayer
    {
        get => _damagedByPlayer;
        set {
            _damagedByPlayer = true;
        }
    }

    private Runner runner;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        runner = GameObject.FindWithTag("Runner").GetComponent<Runner>();
        currentState = new StateMove();
    }

    // Update is called once per frame
    void Update()
    {
        while (currentState.Execute(this))
        {
            currentState = currentState.NextState();
        }
        currentState = currentState.NextState();
    }

    public void Move(Vector3 destionation)
    {
        transform.position = Vector3.MoveTowards(transform.position, destionation, Time.deltaTime * 20);
    }

    //pretty much a stub
    public bool ShouldExplode()
    {
        return false;
    }

    public void Die()
    {
        if (!droppedLoot)
        {
            //generate loot
            for (int i = 0; i < NumDrops; i++)
            {
                runner.CreateLoot(transform.position);
            }
            droppedLoot = true;
        }

        StartCoroutine(explode(true));
    }

    public void Explode()
    {
        isDead = true;
        StartCoroutine(explode(false));
    }

    private IEnumerator explode(bool isDying)
    {
        var exp = GetComponent<ParticleSystem>();
        if (!hasExploded)
        {
            hasExploded = true;
            exp.Play();
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
        if (isDying)
        {
            yield return new WaitForSeconds(0.01f);
            exp.Clear();
        }
        else
        {
            //If exploding need to do a check in radius for things that can be damaged
        }
        runner.DeleteEnemy(gameObject);
        Destroy(gameObject, exp.main.duration);
    }
}
