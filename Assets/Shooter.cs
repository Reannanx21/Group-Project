using UnityEngine;
using Cinemachine;

public class CharacterShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float damage = 10f;

    public AudioClip shootSound;
    private AudioSource audioSource;

    public AudioSource shootAudioSource; // New source for shooting SFX

    public CinemachineImpulseSource impulseSource;

    private Animator animator;
    private float shootCooldown = 0.5f;
    private float timeSinceLastShot = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("No AudioSource found on this object!");

        if (shootAudioSource == null)
            Debug.LogWarning("No external shootAudioSource assigned! Will use fallback.");

        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
            Debug.LogWarning("No CinemachineImpulseSource found!");
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && timeSinceLastShot >= shootCooldown)
        {
            timeSinceLastShot = 0f;

            if (animator != null)
            {
                animator.SetTrigger("Shoot");
                Debug.Log("Shoot animation triggered!");
                // FireProjectile() should be called from animation event
            }
            else
            {
                FireProjectile(); // fallback if no animator
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceLastShot >= shootCooldown)
        {
            timeSinceLastShot = 0f;

            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack animation triggered!");
            }
        }
    }

    // Called from animation event OR fallback
    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Debug.Log("Projectile spawned!");

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }

        // Use external AudioSource if assigned, fallback to internal one
        if (shootSound != null)
        {
            if (shootAudioSource != null)
            {
                shootAudioSource.PlayOneShot(shootSound);
            }
            else if (audioSource != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(); // camera shake!
        }
    }
}
