using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control movement speed
    private Rigidbody2D rb;
    private bool isCollidingWithPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Normal movement logic, if no collision with player
        if (!isCollidingWithPlayer)
        {
            // Example: Move towards a target or in a direction
            Vector2 direction = new Vector2(1f, 0f); // Example direction
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // If colliding with the player, stop or reduce movement
            rb.velocity = Vector2.zero; // This stops the movement
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
