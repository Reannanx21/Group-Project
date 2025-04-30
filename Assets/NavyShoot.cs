using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioClip shootSound; // Step 2: Assign this in the inspector

    public float shootDistance = 5f;
    public float shootCooldown = 1.5f;
    public float bulletSpeed = 10f;

    private float shootTimer;
    private AudioSource audioSource; // Step 1: Reference to AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Initialize AudioSource
    }

    void Update()
    {
        if (player == null || firePoint == null || bulletPrefab == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        shootTimer -= Time.deltaTime;

        if (distance >= shootDistance && shootTimer <= 0f)
        {
            ShootAtPlayer();
            shootTimer = shootCooldown;
        }
    }

    void ShootAtPlayer()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }

        // Step 3: Play the shoot sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
