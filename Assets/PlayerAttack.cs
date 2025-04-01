using Cinemachine;
using UnityEngine;

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

    public void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        if (attackPoint == null) return;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D enemyCollider in enemiesHit)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            EnemyStun enemyStun = enemyCollider.GetComponent<EnemyStun>();
            if (enemyStun != null)
            {
                enemyStun.Stun(2f);
            }
        }

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

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
