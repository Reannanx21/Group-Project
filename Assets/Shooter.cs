using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float damage = 10f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No Animator found on this GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Trigger the shoot animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
                Debug.Log(" Shoot animation triggered!");
            }
            else
            {
                // Fallback: Just shoot immediately
                Shoot();
            }
        }
    }

    // This gets called by the animation event!
    public void FireProjectileFromAnimation()
    {
        Debug.Log(" Animation Event Triggered - Firing Projectile");

        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Missing projectilePrefab or shootPoint!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Debug.Log(" Projectile spawned from animation!");

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }

    // Optional direct call method (fallback if animation fails)
    void Shoot()
    {
        Debug.Log(" Fallback Shoot() called!");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }
}
