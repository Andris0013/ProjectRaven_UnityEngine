using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [Space]

    [SerializeField] float attackRange = 0.5f;



    void Update()
    {
        Attack();
    }

    void Attack()
    {
        //attack animation
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");

        //detect collision
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        //damage detected enemies
        foreach(Collider enemy in hitEnemy)
        {

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
