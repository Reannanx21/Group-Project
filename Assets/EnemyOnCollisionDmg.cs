using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 10f; // Damage amount
    public float damageCooldown = 1f; // Cooldown time between damage
    private float lastDamageTime = 0f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        // Check for "Player" tag (optional)
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    player.health -= damage;
                    lastDamageTime = Time.time;
                    Debug.Log("Player damaged. Current health: " + player.health);
                }
            }
            else
            {
                Debug.LogWarning("playerHealth component is missing on: " + other.gameObject.name);
            }
        }
    }
}