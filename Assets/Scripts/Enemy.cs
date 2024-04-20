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
    [SerializeField] Transform player;

    public Rigidbody Rigidbody { get; private set; }
    Vector3 origin;
    EnemyStates state = EnemyStates.Recovery;
    NavMeshAgent agent;
    float wanderRange = 5;
    Vector3 startingLocation;
    float elapsed = 0;
    float playerSightRange = 5;
    float playerAttackRange = 2;
    float recoveryTime = 2;
    
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
        Debug.Log("Wandering to " + randomPoint);
        if (Vector3.Distance(transform.position, player.position) <= playerSightRange)
        {
            state = EnemyStates.Pursue;
        }
        else
        {
            state = EnemyStates.Recovery;
        }
    }

    void UpdatePursue()
    {
        agent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) > playerSightRange)
        {
            state = EnemyStates.Wander;
        }
        else if (Vector3.Distance(transform.position, player.position) <= playerAttackRange)
        {
            state = EnemyStates.Attack;
        }
    }

    void UpdateAttack()
    {
        Rigidbody.AddForce((transform.position - player.position).normalized * 5);
        Debug.Log("Jeff attacks!");
        state = EnemyStates.Recovery;
    }

    void UpdateRecovery()
    {
        elapsed += Time.deltaTime;
        if (elapsed > recoveryTime) {
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
