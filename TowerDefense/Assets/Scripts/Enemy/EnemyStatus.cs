using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float MaxHealth = 10.0f;

    private float currentHealth;
    private bool isDead;
    private bool hasExploded = false;

    private Runner runner;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        isDead = false;
        runner = GameObject.FindWithTag("Runner").GetComponent<Runner>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (isDead)
        {
            isDead = true;
        }

    }

    public void applyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0 && !isDead)
        {
            die();
        }
    }

    public void die()
    {
        isDead = true;
        StartCoroutine(explode(true));

        //generate loot
        runner.CreateLoot(transform.position);
    }

    public void Explode()
    {
        isDead = true;
        StartCoroutine(explode(false));
    }

    private IEnumerator explode(bool isDying)
    {
        //Need to remove from list of enemies here or smth
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
        }
        exp.Clear();
        Destroy(gameObject, exp.main.duration);
    }
}
