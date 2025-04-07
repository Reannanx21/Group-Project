using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private bool hasTriggered;

    private CoinManager coinManager;

    private void Start()
    {
        // Safer reference in case CoinManager isn't ready at Start
        coinManager = CoinManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            // Safety check in case the manager hasn't been found
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

            Destroy(gameObject);
        }
    }
}
