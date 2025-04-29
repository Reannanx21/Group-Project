using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Bullet hit Player!");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy") && gameObject.tag == "PlayerBullet") // reflected shot hits enemy
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // assume you have this method
                Debug.Log("Bullet hit Enemy!");
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Debug.Log("Bullet hit non-trigger object, destroyed!");
            Destroy(gameObject);
        }
    }
}
