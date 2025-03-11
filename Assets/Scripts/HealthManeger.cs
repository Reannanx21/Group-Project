using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Starting health
    public float maxHealth = 100f;  // Maximum health
    public Image healthBar;  // Health bar UI element

    private float lastHealth = -1f;  // Tracks the last health value

    void Start()
    {
        if (maxHealth == 0)
        {
            maxHealth = 100f;
        }

        health = maxHealth;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            UpdateHealthBar();
        }
    }

    void Update()
    {
        if (health != lastHealth)
        {
            if (healthBar != null)
            {
                UpdateHealthBar();
            }
            lastHealth = health;
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar == null) return;

        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        // Force UI refresh
        healthBar.canvasRenderer.SetAlpha(1.0f);
        healthBar.CrossFadeAlpha(1.0f, 0, true);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
        UpdateHealthBar();
    }
}