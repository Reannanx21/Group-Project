using UnityEngine;

public class MughalMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float detectionRange = 10f; // Detection range where enemy starts following
    public float stopDistance = 3f;    // Minimum distance to the player before the enemy stops moving

    private Camera mainCam;
    private Rigidbody2D rb;
    private Animator animator;

    public float groundDistance = 0.5f;
    public float gravityScale = 3f;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = gravityScale;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance);

        if (hit.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.5f); // Apply small downward velocity if not grounded
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(CameraShake(0.15f, 0.3f)); // Camera shake effect when hit
    }

    void Die()
    {
        // Handle enemy death, like triggering a death animation
        Destroy(gameObject, 2f); // Destroy after delay to give time for animations
    }

    System.Collections.IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCam.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCam.transform.localPosition = originalPos;
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection range and ground distance in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Detection range
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance); // Ground check
    }
}
