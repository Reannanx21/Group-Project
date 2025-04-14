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
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        bool enemyHit = false;

        if (attackPoint != null)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

            foreach (Collider2D hit in enemiesHit)
            {
                // Enemy damage
                EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    enemyHit = true;
                }

                // Enemy stun
                EnemyStun enemyStun = hit.GetComponent<EnemyStun>();
                if (enemyStun != null)
                {
                    enemyStun.Stun(2f);
                    enemyHit = true;
                }

                // Destructible object damage
                if (hit.CompareTag("Destructible"))
                {
                    ObjectHealth objectHealth = hit.GetComponent<ObjectHealth>();
                    if (objectHealth != null)
                    {
                        objectHealth.TakeDamage(damage);
                        enemyHit = true;
                    }
                }
            }
        }

        if (enemyHit && impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }

    public void ResetAttack() // Call this from animation event
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