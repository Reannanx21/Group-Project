using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control movement speed
    private Rigidbody2D rb;
    private bool isCollidingWithPlayer = false;

    private Animator anim; // Reference to Animator
    public Transform player; // Reference to the player’s Transform

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Ensure there's a player reference
        if (player == null) return;

        // Normal movement logic, if no collision with player
        if (!isCollidingWithPlayer)
        {
            // Move towards the player’s position
            Vector2 direction = (player.position - transform.position).normalized; // Direction towards the player
            rb.velocity = direction * moveSpeed;

            // Trigger walking animation if the enemy is moving
            if (rb.velocity.magnitude > 0)
            {
                anim.SetBool("IsWalking", true); // Set walking animation
            }
            else
            {
                anim.SetBool("IsWalking", false); // Stop walking animation
            }
        }
        else
        {
            // If colliding with the player, stop or reduce movement
            rb.velocity = Vector2.zero; // This stops the movement
            anim.SetBool("IsWalking", false); // Stop walking animation when colliding with player
        }
    }

    // Called when the collision with another collider starts
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detect when the enemy hits the player
            isCollidingWithPlayer = true;
        }
    }

    // Called when the collision is ongoing
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Prevent the enemy from being pushed away or moving toward the player
            rb.velocity = Vector2.zero; // Or adjust as needed
        }
    }

    // Called when the collision with another collider ends
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
        }
    }
}
