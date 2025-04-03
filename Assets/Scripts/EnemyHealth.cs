using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool isDead = false; // Prevents multiple deaths

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (anim == null)
            Debug.LogError(" No Animator found on " + gameObject.name);

        if (boxCollider == null)
            Debug.LogError(" No BoxCollider2D found on " + gameObject.name);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Prevents further damage after death

        health -= amount;
        health = Mathf.Max(health, 0); // Prevents negative health

        Debug.Log(gameObject.name + " took damage! New health: " + health);

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Prevent multiple deaths
        isDead = true;

        Debug.Log(gameObject.name + " has died! Playing death animation...");

        // Disable collider so enemy can't interact
        if (boxCollider != null)
            boxCollider.enabled = false;

        // Play death animation
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        // Destroy after animation plays
        Destroy(gameObject, 1.5f); // Adjust time based on animation length
    }

    void Update()
    {
        // Debugging: Press "K" to instantly kill enemy
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(9999);
        }
    }
}
