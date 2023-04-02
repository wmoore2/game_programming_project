using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int NumDrops = 1;
    private bool isDead;
    private bool hasExploded = false;
    private bool droppedLoot = false;
    private State currentState;

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    private Runner runner;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        runner = GameObject.FindWithTag("Runner").GetComponent<Runner>();
        ChangeState(new StateMove());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute(this);

    }

    public void Move()
    {
        IPathFinder pfinder = GameObject.FindObjectOfType<PathFinderRepository>().GetPathFinder(PathFinderType.HeartFinder);
        var targetPos = pfinder.NextStep(transform.position);
        if (targetPos is not null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.Value, Time.deltaTime * 20);
        }
        else
        {
            
        }
    }

    //pretty much a stub
    public bool ShouldExplode()
    {
        return false;
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
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
