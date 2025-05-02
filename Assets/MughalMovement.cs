using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashCooldown = 5f;
    public float dashDuration = 0.5f;
    public float detectionRange = 10f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;

    public float recoilDistance = 1f;
    public float recoilTime = 0.2f;

    public float damage = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isRecoiling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f && !isDashing)
            {
                StartCoroutine(DashTowardsPlayer(direction));
                dashCooldownTimer = dashCooldown;
            }
        }

        if (Vector2.Distance(transform.position, player.position) < 2f)
        {
            rb.velocity = Vector2.zero;
        }

        if (!isDashing && !isRecoiling)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            MoveEnemy(direction);
        }

        animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);
    }

    IEnumerator DashTowardsPlayer(Vector2 direction)
    {
        isDashing = true;
        float dashTime = 0f;

        while (dashTime < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            dashTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    void MoveEnemy(Vector2 direction)
    {
        Vector2 targetVelocity = direction * moveSpeed;
        rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Enemy collided with the player and dealt {damage} damage!");
            }

            if (!isRecoiling)
            {
                StartCoroutine(RecoilEffect());
            }
        }
    }

    IEnumerator RecoilEffect()
    {
        isRecoiling = true;
        Vector2 recoilDirection = (transform.position - player.position).normalized;
        Vector2 recoilVector = recoilDirection * recoilDistance;

        rb.velocity = recoilVector;

        yield return new WaitForSeconds(recoilTime);

        rb.velocity = Vector2.zero;
        isRecoiling = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
