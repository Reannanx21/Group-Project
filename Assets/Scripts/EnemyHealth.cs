using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private bool isDead = false;
    private CameraShake cameraShake;

    private void Start()
    {
        // Debugging to ensure all required components are properly attached
        cameraShake = CameraShake.Instance;
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Debugging component check
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake component is missing on " + gameObject.name);
        }
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider2D component is missing on " + gameObject.name);
        }
    }

    public void TakeDamage(float amount)
    {
        // Debugging the damage taken
        if (isDead)
        {
            Debug.LogWarning($"{gameObject.name} is already dead. Damage will not be applied.");
            return;
        }

        health -= amount;

        // Debugging damage info
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {health}");

        // Camera shake effect
        if (cameraShake != null)
        {
            cameraShake.TriggerShake(0.3f, 0.2f);
        }
        else
        {
            Debug.LogError("CameraShake is null, unable to trigger shake effect!");
        }

        // Check if enemy is dead
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Debugging death
        Debug.Log($"{gameObject.name} has died!");

        // Destroy the enemy game object
        Destroy(gameObject);

        // Optionally, handle animations or other death logic
        // If animator is assigned, set the "isDead" state to true
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            Debug.LogError("Animator is null, unable to play death animation!");
        }

        // Optionally, disable colliders if needed
        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;
        }
        else
        {
            Debug.LogError("CapsuleCollider2D is null, unable to disable it during death!");
        }
    }
}
