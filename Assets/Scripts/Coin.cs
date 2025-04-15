using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private bool hasTriggered;

    private CoinManager coinManager;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D coinCollider;

    private void Start()
    {
        coinManager = CoinManager.instance;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (coinManager == null)
            {
                coinManager = CoinManager.instance;
            }

            if (coinManager != null)
            {
                coinManager.ChangeCoins(value);
            }
            else
            {
                Debug.LogWarning("CoinManager not found! Make sure it exists and is initialized.");
            }

            
            if (spriteRenderer != null)
                spriteRenderer.enabled = false;

            if (coinCollider != null)
                coinCollider.enabled = false;

            if (audioSource != null)
            {
                audioSource.Play();
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject); // no sound, destroy immediately
            }
        }
    }
}
