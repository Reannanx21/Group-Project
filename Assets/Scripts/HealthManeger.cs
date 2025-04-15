using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Image healthBar;
    public bool isDead = false;
    public GameObject restartScreen; // Assign in Inspector
    [SerializeField] private float debugDamageAmount = 20.0f;

    void Start()
    {
        if (maxHealth <= 0)
        {
            maxHealth = 100f; // Ensure maxHealth is never zero
        }

        health = maxHealth;
        InitializeHealthBar();

        if (restartScreen != null)
        {
            restartScreen.SetActive(false); // Hide restart screen initially
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("DebugTakeDamage"))
        {
            TakeDamage(debugDamageAmount);
        }

        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }

    void InitializeHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.canvasRenderer.SetAlpha(1f);
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null && maxHealth > 0)
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0f, 1f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health = Mathf.Clamp(health - damage, 0, maxHealth);
        UpdateHealthBar();

        if (health <= 0) Die();
    }

    public void Heal2(float healAmount)
    {
        if (isDead) return;

        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        Debug.Log("Healed current health" + health);
        UpdateHealthBar();
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Time.timeScale = 0f; // Pause game
        if (restartScreen != null)
        {
            restartScreen.SetActive(true);
        }
    }

    void RestartScene()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
