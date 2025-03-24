using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.name); // Debug the object that collides

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!"); // Confirms player was correctly identified

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Healing player by: " + healAmount); // Confirms playerHealth is found
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player!");
            }
        }
    }
}
