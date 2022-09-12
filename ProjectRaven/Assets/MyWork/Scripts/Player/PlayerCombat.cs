using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))            // stop character from moving when animation is active
            animator.SetTrigger("Attack");

        // detect collision

        //damage layers with layermask
    }
    
}
