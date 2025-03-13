using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;
    public float damage = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to attack
        {
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Attack function called!"); // ✅ Check if Attack() is triggered

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        Debug.Log($"Enemies detected: {enemiesHit.Length}"); // ✅ Check how many enemies are found

        foreach (Collider2D enemyCollider in enemiesHit)
        {
            if (enemyCollider != null)
            {
                Debug.Log($"Hit Enemy: {enemyCollider.name}"); // ✅ Confirm which enemy is hit

                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
                else
                {
                    Debug.LogError("EnemyHealth component not found on: " + enemyCollider.name);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
    }
}