using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform Player;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Rigidbody forDeath;
    [SerializeField] new ParticleSystem particleSystem;
    [Space]

    [SerializeField] float lookRadius = 10f;
    [SerializeField] float maxHealth = 1;
    [SerializeField] float attackRange;
    [SerializeField] float enemyDamage = 100f;

    float currentHealth;

    void Start()
    {
        this.enabled = true;
        currentHealth = maxHealth;
        forDeath = GetComponentInChildren<Rigidbody>();
        forDeath.isKinematic = true;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Pause();
    }


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

        DamagePlayer();
    }




    // Support

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void ChasePlayer()
    {
        agent.SetDestination(Player.position);

        // look at player if needed
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        particleSystem.Play();

        forDeath.isKinematic = false;
        GetComponentInChildren<SphereCollider>().enabled = false;
        this.enabled = false;
    }

    void DamagePlayer()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Player.GetComponentInChildren<PlayerControls>().TakeDamage(enemyDamage);
        }
    }
}


/*

[SerializeField] float attackRadius = 1f;            if needed for actual enemy character with animation

if (distance < attackRadius)
{
    AttackPlayer();
}

void AttackPlayer()      if needed for actual enemy character with animation

Gizmos.color = Color.red;
Gizmos.DrawWireSphere(transform.position, attackRadius);


*/
