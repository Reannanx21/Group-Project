using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;

    // Reference to CameraShake (singleton access)
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = CameraShake.Instance;  // Accessing the CameraShake singleton
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {health}");

        // Trigger camera shake when the enemy takes damage
        cameraShake.TriggerShake(0.3f, 0.2f);  // Shake with magnitude 0.3 and duration 0.2 seconds

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }
}
