using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public float parryTime = 0.2f;  // Regular parry window
    public float perfectParryTime = 0.1f;  // Perfect parry window
    public float parryCooldown = 1.0f;
    public int healAmount = 10;  // Heal amount for perfect parry
    public float parryRadius = 2f;  // Size of the parry zone

    private bool canParry = true;
    private bool isParrying = false;
    private float parryStartTime;

    private PlayerHealth playerHealth;

    public GameObject parryIndicator;  // Optional: Assign a visual effect in the Inspector
    private Collider2D parryCollider;  // Reference to the Parry Zone's Collider2D

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();  // Get health script on player
        parryCollider = transform.Find("ParryZone").GetComponent<Collider2D>();  // Get the ParryZone's Collider
    }

    void Update()
    {
        if (canParry && Input.GetMouseButtonDown(1)) // Right-click to parry
        {
            StartParry();
        }
    }

    void StartParry()
    {
        isParrying = true;
        canParry = false;
        parryStartTime = Time.time;
        Debug.Log("Parry Active!");

        if (parryIndicator != null)
            parryIndicator.SetActive(true); // Show visual effect

        Invoke(nameof(EndParry), parryTime);
        Invoke(nameof(ResetParry), parryCooldown);
    }

    void EndParry()
    {
        isParrying = false;
        if (parryIndicator != null)
            parryIndicator.SetActive(false); // Hide visual effect
    }

    void ResetParry()
    {
        canParry = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isParrying && other.CompareTag("EnemyAttack"))
        {
            float parryDuration = Time.time - parryStartTime;

            if (parryDuration <= perfectParryTime)
            {
                Debug.Log("Perfect Parry! Healing...");
                playerHealth.Heal(healAmount);  // Heals player
            }
            else
            {
                Debug.Log("Regular Parry!");
            }

            Destroy(other.gameObject); // Destroy enemy attack
        }
    }

    void OnDrawGizmosSelected()
    {
        // Check if the parryCollider exists to avoid null reference errors
        if (parryCollider != null)
        {
            // Add an offset to the position of the gizmo to move it up above the player
            Vector3 offset = new Vector3(0, 1f, 0);  // Adjust this value as needed
            Gizmos.color = isParrying ? Color.green : Color.red; // Green for parrying, Red for idle
            Gizmos.DrawWireSphere(parryCollider.transform.position + offset, parryRadius); // Draw the gizmo at the parry zone's position
        }
    }
}