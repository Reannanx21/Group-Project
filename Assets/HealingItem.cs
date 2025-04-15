using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int healAmount = 20;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Healing player by: " + healAmount);
                playerHealth.Heal(healAmount);

                if (audioSource != null)
                {
                    audioSource.Play();

                   
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<Collider2D>().enabled = false;

                    // Destroy after sound finishes
                    Destroy(gameObject, audioSource.clip.length);
                }
                else
                {
                    Destroy(gameObject); // fallback
                }
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player!");
            }
        }
    }
}
