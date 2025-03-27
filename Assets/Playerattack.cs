using Cinemachine;
using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemies;
    public float damage = 10f;
    private bool isAttacking = false;

    public CinemachineImpulseSource impulseSource;  // Reference to Cinemachine Impulse Source

    // Combine the logic from multiple Start() methods into one
    void Start()
    {
        anim = GetComponent<Animator>();  // Initialize the animator
        if (anim == null)
        {
            Debug.LogError("Animator is missing on the player!");
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();  // Get the Impulse Source component
        if (impulseSource == null)
        {
            Debug.LogError("CinemachineImpulseSource is missing!");
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

        yield return new WaitForSeconds(0.5f);  

        isAttacking = false;
    }

    // This is called via an Animation Event
    public void Attack()
    {
        Debug.Log("Attack triggered!");

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

        // Trigger Cinemachine Impulse
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();  // Trigger the impulse shake
        }
        else
        {
            Debug.LogError("ImpulseSource is not assigned!");
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
