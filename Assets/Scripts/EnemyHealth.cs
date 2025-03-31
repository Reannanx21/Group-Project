using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;


    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = CameraShake.Instance;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {health}");


        cameraShake.TriggerShake(0.3f, 0.2f);

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