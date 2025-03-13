using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Image healthBar;
    private bool isDead = false;

    void Start()
    {
        if (maxHealth <= 0)
        {
            maxHealth = 100f; // Ensure maxHealth is never zero
        }

        health = maxHealth;
        InitializeHealthBar();
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
        if (healthBar != null && maxHealth > 0) // Prevent division by zero
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

    public void Heal(float healAmount)
    {
        if (isDead) return;

        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Invoke("ReloadScene", 1f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}