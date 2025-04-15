using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float damage = 10f;

    private Animator animator;
    private bool isActionLocked = false;

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
        if (isActionLocked) return; // Block input during other animations

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
                Debug.Log("Shoot animation triggered!");
                LockAction();
            }
            else
            {
                Shoot(); // fallback
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left click for attack?
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack animation triggered!");
                LockAction();
            }
        }
    }

    public void FireProjectileFromAnimation()
    {
        Debug.Log("Animation Event Triggered - Firing Projectile");

        if (projectilePrefab == null || shootPoint == null)
        {
            Debug.LogWarning("Missing projectilePrefab or shootPoint!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Debug.Log("Projectile spawned from animation!");

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }

    void Shoot()
    {
        Debug.Log("Fallback Shoot() called!");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }
    }

    //  Call this at the start of any action
    void LockAction()
    {
        isActionLocked = true;
    }

    //  Call this from an animation event at the END of shoot/attack
    public void UnlockAction()
    {
        Debug.Log("Action unlocked");
        isActionLocked = false;
    }
}
