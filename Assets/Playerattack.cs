using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemies;
    public float damage = 10f;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is missing on the player!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f); // Adjust based on animation

        isAttacking = false;
    }

    // This is called via an Animation Event
    public void Attack()
    {
        Debug.Log("Attack() function triggered!");

        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint is not assigned!");
            return;
        }

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        Debug.Log($"Enemies hit: {enemiesHit.Length}");

        foreach (Collider2D enemyCollider in enemiesHit)
        {
            if (enemyCollider != null)
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    Debug.Log($"Damaging enemy: {enemyCollider.name} for {damage} damage");
                    enemyHealth.TakeDamage(damage);
                }
                else
                {
                    Debug.LogWarning($"Enemy {enemyCollider.name} has no EnemyHealth script!");
                }

                // Apply stun directly to the enemy itself
                EnemyStun enemyStun = enemyCollider.GetComponent<EnemyStun>();
                if (enemyStun != null)
                {
                    Debug.Log($"Stunning enemy {enemyCollider.name}!");
                    enemyStun.Stun(2f);
                }
            }
        }

        // Trigger camera shake
        if (CameraShake.Instance != null)
        {
            Debug.Log("Camera shake triggered!");
            CameraShake.Instance.TriggerShake(0.3f, 0.2f);
        }
        else
        {
            Debug.LogError("CameraShake.Instance is NULL!");
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
