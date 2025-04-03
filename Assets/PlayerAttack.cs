using UnityEngine;
using Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemies;
    public float damage = 10f;
    public bool isAttacking = false;

    public CinemachineImpulseSource impulseSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true; // Prevent queuing attacks

        anim.SetTrigger("Attack");

        bool enemyHit = false; // Track if an enemy was hit

        if (attackPoint != null)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

            foreach (Collider2D enemyCollider in enemiesHit)
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    enemyHit = true;
                }

                EnemyStun enemyStun = enemyCollider.GetComponent<EnemyStun>();
                if (enemyStun != null)
                {
                    enemyStun.Stun(2f);
                    enemyHit = true;
                }
            }
        }

        // Shake camera only if an enemy was hit
        if (enemyHit && impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }

    public void ResetAttack() // Call this from animation
   {
       isAttacking = true;
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