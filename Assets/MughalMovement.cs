using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;  // Dash speed
    public float dashCooldown = 5f; // Dash cooldown
    public float dashDuration = 0.5f; // Dash duration
    public float detectionRange = 10f;  // Detection range to start following player
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;

    public float recoilDistance = 1f;  // Distance the enemy moves back when colliding with the player
    public float recoilTime = 0.2f;  // Duration for recoil effect

    public float damage = 10f;  // Damage to deal to the player when collision occurs, now editable in the Inspector

    private Rigidbody2D rb;
    private Animator animator;
    private bool isRecoiling = false; // Flag to check if the enemy is in recoil

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Calculate distance from the player
        float distance = Vector2.Distance(transform.position, player.position);

        // If within detection range, follow the player
        if (distance <= detectionRange)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Handle dash logic (every dashCooldown seconds)
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f && !isDashing)
            {
                StartCoroutine(DashTowardsPlayer(direction));
                dashCooldownTimer = dashCooldown;  // Reset cooldown timer
            }
        }

        // Optional: Stop movement when player is within a certain distance (e.g., attack range)
        if (Vector2.Distance(transform.position, player.position) < 2f)
        {
            rb.velocity = Vector2.zero;
        }

        // Handle regular movement (walking) when not recoiling or dashing
        if (!isDashing && !isRecoiling)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            MoveEnemy(direction);
        }

        // Optional: Set walking animation based on movement
        animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);
    }

    IEnumerator DashTowardsPlayer(Vector2 direction)
    {
        isDashing = true;
        float dashTime = 0f;

        // Dash towards player for dashDuration
        while (dashTime < dashDuration)
        {
            rb.velocity = direction * dashSpeed;
            dashTime += Time.deltaTime;
            yield return null;
        }

        // Stop the dash
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    void MoveEnemy(Vector2 direction)
    {
        // Regular movement
        Vector2 targetVelocity = direction * moveSpeed;
        rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with the player
        if (collision.collider.CompareTag("Player"))
        {
            // Assuming the player has a method to handle damage
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Damage the player (now using the public 'damage' value)
                Debug.Log($"Enemy collided with the player and dealt {damage} damage!");
            }

            // Recoil effect: Move back slightly and pause for recoil time
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

        // Apply recoil by moving the enemy back slightly
        rb.velocity = recoilVector;

        // Wait for recoil duration before resuming movement
        yield return new WaitForSeconds(recoilTime);

        // Stop recoil and resume normal movement
        rb.velocity = Vector2.zero;
        isRecoiling = false;
    }

    private void OnDrawGizmos()
    {
        // Optional: Draw detection range for debugging
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
