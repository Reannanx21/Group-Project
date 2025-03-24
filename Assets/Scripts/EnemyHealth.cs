using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f; // Default enemy health

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"TakeDamage called on {gameObject.name}"); // Confirm function is called 
        health -= damageAmount;
        Debug.Log($"Enemy took {damageAmount} damage, remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}