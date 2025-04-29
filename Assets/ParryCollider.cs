using UnityEngine;

public class ParryCollider : MonoBehaviour
{
    private ParrySystem parrySystem;

    void Start()
    {
        parrySystem = GetComponentInParent<ParrySystem>();
        if (parrySystem == null)
            Debug.LogError("ParrySystem not found in parent!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!parrySystem.IsParrying)
        {
            return; // Only react if parrying is active
        }

        Debug.Log($"Parry Collider Triggered by: {other.name}");

        if (other.CompareTag("EnemyBullet"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Debug.Log($"Parried Bullet: {other.name}, Velocity before: {rb.velocity}");
                rb.velocity = -rb.velocity;
                other.tag = "PlayerBullet";  // Change the bullet tag to reflect the new state
                other.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");  // Optional: Change layer
                Debug.Log($"Parried Bullet: {other.name}, Velocity after: {rb.velocity}");
            }
        }
    }
}
