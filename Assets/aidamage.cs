using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10; // Amount of damage dealt to the player
    public float attackCooldown = 1.5f; // Cooldown time between attacks

    private bool canAttack = true; // Control when the enemy can attack

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}