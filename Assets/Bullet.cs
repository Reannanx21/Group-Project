using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private float damage;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collided with: " + other.name);

        // Damage enemy if it has EnemyHealth
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log("Enemy hit! Applying " + damage + " damage.");
            enemy.TakeDamage(damage);
        }

        // Damage object if it has ObjectHealth
        ObjectHealth obj = other.GetComponent<ObjectHealth>();
        if (obj != null)
        {
            Debug.Log("Object hit! Applying " + damage + " damage.");
            obj.TakeDamage(damage);
        }

        // Optional: Stun enemies only
        EnemyStun enemyStun = other.GetComponent<EnemyStun>();
        if (enemyStun != null)
        {
            float stunDuration = 2f;
            Debug.Log("Stunning enemy for " + stunDuration + " seconds.");
            enemyStun.Stun(stunDuration);
        }

        Destroy(gameObject);
    }
}
