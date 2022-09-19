using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Player;
    [Space]

    [SerializeField] float lookRadius = 10f;
    [SerializeField] float attackRadius = 1f;


    void Update()
    {     
        if (Player == null)
            return;
        if (agent == null)
            return;

        float distance = Vector3.Distance(Player.position, transform.position);


        if (distance < lookRadius)
        {
            ChasePlayer();
        }
        if (distance < attackRadius)
        {
            AttackPlayer();
        }

    }




    // Support


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }


    void ChasePlayer()
    {
        // Debug.Log("Gotcha");
        agent.SetDestination(Player.position);
    }

    void AttackPlayer()
    {

    }
}
