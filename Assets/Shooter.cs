using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float damage = 10f;

    public AudioClip shootSound;  // Assign this in the inspector
    private AudioSource audioSource;

    private Animator animator;
    private float shootCooldown = 0.5f;  // Time in seconds between shots
    private float timeSinceLastShot = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No Animator found on this GameObject.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on this GameObject.");
        }
    }

    void Update()
    {
        // Update the time since last shot
        timeSinceLastShot += Time.deltaTime;

        // If the player presses E and enough time has passed, shoot
        if (Input.GetKeyDown(KeyCode.E) && timeSinceLastShot >= shootCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
                Debug.Log("Shoot animation triggered!");
                FireProjectile();
                timeSinceLastShot = 0f; // Reset the cooldown timer
            }
            else
            {
                Shoot();  // Fallback if there's no animator
            }
        }

        // If the player clicks left mouse button, perform an attack (similar to shooting)
        if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastShot >= shootCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack animation triggered!");
                timeSinceLastShot = 0f;  // Reset the cooldown timer
            }
        }
    }

    // Fire the projectile and play sound
    void FireProjectile()
    {
        Debug.Log("Firing projectile");

        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Missing projectilePrefab or shootPoint!");
            return;
        }

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Debug.Log("Projectile spawned!");

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }

    // Fallback Shoot if no animator or animation events are set
    void Shoot()
    {
        Debug.Log("Fallback Shoot() called!");

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }
}
