using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private int coins;
    [SerializeField] private TMP_Text coinsDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }


       

    }

    private void Start()
    {
        UpdateCoinDisplay();
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (coinsDisplay != null)
        {
            coinsDisplay.text = coins.ToString();
        }
        else
        {
            Debug.LogWarning("Coins Display TextMeshPro is not assigned in the Inspector!");
        }
    }
    
}
