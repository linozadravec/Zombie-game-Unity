using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 100f;
    [SerializeField] float timeTillDestroyObject = 10f;

    NavMeshAgent navMeshAgent;
    EnemyAI enemyAI;

    bool isDead = false;

    /*
    public bool IsDead()
    {
        return isDead;
    }
    */
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        DisableComponents();
        Destroy(gameObject, timeTillDestroyObject);
    }

    private void DisableComponents()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
