using UnityEngine;
using System.Collections;
public class EnemyDashAndDamage : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float stopDistance = 3f;
    public float dashSpeed = 15f;  // Speed of the dash
    public float dashCooldown = 5f; // Dash every 5 seconds
    public float dashDuration = 0.5f; // How long the dash lasts
    public float detectionRange = 10f;  // Detection range to start following the player
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

        // Calculate distance from player
        float distance = Vector2.Distance(transform.position, player.position);

        // Only follow the player if within detection range
        if (distance <= detectionRange)
        {
            // Calculate direction to player
            Vector2 direction = (player.position - transform.position).normalized;

            // If the player is farther than the stop distance, move towards them
            if (distance > stopDistance)
            {
                MoveEnemy(direction);
            }
            // If the player is closer than the stop distance, stop moving
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y); // Stop movement on X-axis
            }

            animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);

            // Handle the dash every dashCooldown seconds
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f && !isDashing)
            {
                StartCoroutine(DashTowardsPlayer(direction));
                dashCooldownTimer = dashCooldown;  // Reset the cooldown
            }
        }
        else
        {
            // Optional: Set idle state if not following player
            animator.SetBool("isWalking", false);
        }

        // Keep the enemy grounded
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
            rb.velocity = new Vector2(rb.velocity.x, -0.5f); // Apply small downward velocity if not grounded
        }
    }

    IEnumerator DashTowardsPlayer(Vector2 direction)
    {
        isDashing = true;

        // Start the dash by moving the enemy fast towards the player
        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            dashTime += Time.deltaTime;
            yield return null;
        }

        // Stop the dash movement
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with the player
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Deal damage to player
                playerHealth.TakeDamage(10); // Adjust the damage as needed
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
        // Handle enemy death (e.g., triggering a death animation, etc.)
        Destroy(gameObject, 2f);  // Destroy after a short delay
    }

    private void OnDrawGizmos()
    {
        // Draw detection range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);  // Detection range
    }
}
