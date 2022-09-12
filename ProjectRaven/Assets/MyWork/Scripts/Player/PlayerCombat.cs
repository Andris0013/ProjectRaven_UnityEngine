using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    void Attack()
    {
        animator.Play("Attack");
    }
}
