using UnityEngine;
using System.Collections;

public class EnemyDashAndDamage : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float stopDistance = 3f;
    public float dashSpeed = 15f;
    public float dashCooldown = 5f;
    public float dashDuration = 0.5f;
    public float detectionRange = 10f;
    public LayerMask playerLayer;

    private Camera mainCam;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        mainCam = Camera.main;
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

            if (distance > stopDistance)
            {
                MoveEnemy(direction);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);

            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f && !isDashing)
            {
                StartCoroutine(DashTowardsPlayer(direction));
                dashCooldownTimer = dashCooldown;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        KeepOnGround();
    }

    void MoveEnemy(Vector2 direction)
    {
        Vector2 targetVelocity = direction * moveSpeed;
        rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
    }

    void KeepOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);

        if (hit.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                Debug.Log("Enemy hit the player!");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, 2f);
    }
}
