using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshUIReference();
    }

    private void Start()
    {
        FindCoinTextIfNeeded();
        UpdateCoinUI();
    }

    private void FindCoinTextIfNeeded()
    {
        if (coinsDisplay == null)
        {
            GameObject displayObject = GameObject.FindWithTag("CoinsDisplay");
            if (displayObject != null)
            {
                coinsDisplay = displayObject.GetComponent<TMP_Text>();
                Debug.Log("Coin UI found and assigned.");
            }
            else
            {
                Debug.LogWarning("No coin display found in scene!");
            }
        }
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinsDisplay != null)
        {
            coinsDisplay.text = coins.ToString();
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public void RefreshUIReference()
    {
        FindCoinTextIfNeeded();
        UpdateCoinUI();
    }
}
