using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum EnemyStates
{
    Wander,
    Pursue,
    Attack,
    Recovery
}

public class Enemy : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    Vector3 origin;
    EnemyStates state = EnemyStates.Recovery;
    NavMeshAgent agent;
    float wanderRange = 5;
    Vector3 startingLocation;
    float elapsed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        origin = transform.position;
        agent = GetComponent<NavMeshAgent>();
        startingLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current state: " + state);
        switch (state)
        {
            case EnemyStates.Wander:
                UpdateWander();
                break;
            case EnemyStates.Pursue:
                UpdatePursue();
                break;
            case EnemyStates.Attack:
                UpdateAttack();
                break;
            case EnemyStates.Recovery:
                UpdateRecovery();
                break;
        }
    }

    void UpdateWander()
    {
        Vector3 randomPoint = GetRandomPoint();
        agent.SetDestination(randomPoint);
        Debug.Log(randomPoint);
        state = EnemyStates.Recovery;
    }

    void UpdatePursue()
    {

    }

    void UpdateAttack()
    {

    }

    void UpdateRecovery()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 2) {
            state = EnemyStates.Pursue;
            elapsed = 0;
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 offset = new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange));

        NavMeshHit hit;
        bool gotPoint = NavMesh.SamplePosition(startingLocation + offset, out hit, 1, NavMesh.AllAreas);

        if (gotPoint)
        {
            return hit.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void ApplyKnockback(Vector3 knockback)
    {
        GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse);
    }

    public void Respawn()
    {
        transform.position = origin;
    }
}
