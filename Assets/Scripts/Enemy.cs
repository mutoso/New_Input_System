using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    EnemyStates state;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    void UpdatePursue()
    {

    }

    void UpdateAttack()
    {

    }

    void UpdateRecovery()
    {

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
