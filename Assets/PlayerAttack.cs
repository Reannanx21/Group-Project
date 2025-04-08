using UnityEngine;
using Cinemachine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemies;
    public LayerMask destructibles; 
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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;

        anim.SetTrigger("Attack");

        bool anythingHit = false;

        if (attackPoint != null)
        {
            // Hit enemies
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
            foreach (Collider2D enemyCollider in enemiesHit)
            {
                EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    anythingHit = true;
                }

                EnemyStun enemyStun = enemyCollider.GetComponent<EnemyStun>();
                if (enemyStun != null)
                {
                    enemyStun.Stun(2f);
                    anythingHit = true;
                }
            }

            // Hit destructibles
            Collider2D[] destructibleHits = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, destructibles);
            foreach (Collider2D obj in destructibleHits)
            {
                

                // If it has ObjectHealth, damage it
                ObjectHealth health = obj.GetComponent<ObjectHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    anythingHit = true;
                }
            }
        }

        if (anythingHit && impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
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
