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

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is missing on the player!");
        }

        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            Debug.LogError("CinemachineImpulseSource is missing! Did you assign it in the Inspector?");
        }
        else
        {
            Debug.Log("CinemachineImpulseSource found.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Debug.Log("Attack button pressed!");
            StartCoroutine(AttackRoutine());
        }

        // Debugging: Manually trigger impulse with 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Manually triggering impulse from Update()!");
            if (impulseSource != null)
            {
                impulseSource.GenerateImpulse(10f);  // Stronger impulse for testing
            }
            else
            {
                Debug.LogError("Impulse Source is NULL when pressing P!");
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        Debug.Log("Attack animation triggered.");

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        Debug.Log("Attack animation completed.");
    }

    // Called via Animation Event
    public void Attack()
    {
        Debug.Log("Attack function triggered!");

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
            Debug.Log("Triggering camera shake!");
            impulseSource.GenerateImpulse(10f); // Stronger shake for testing
        }
        else
        {
            Debug.LogError("ImpulseSource is NULL! Did you assign it?");
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
