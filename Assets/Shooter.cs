using UnityEngine;
using Cinemachine;
using System.Collections;

public class CharacterShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float damage = 10f;

    public AudioClip shootSound;
    private AudioSource audioSource;

    public AudioSource shootAudioSource;

    public CinemachineImpulseSource impulseSource;

    private Animator animator;

    private bool isOnCooldown = false;
    public float cooldownTime = 5f;

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
        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
                Debug.Log("Shoot animation triggered!");
            }
            else
            {
                FireProjectile();
            }

            StartCoroutine(ShootCooldown());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isOnCooldown)
        {
            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack animation triggered!");
            }
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Debug.Log("Projectile spawned!");

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
        }

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
            impulseSource.GenerateImpulse();
        }
    }

    private IEnumerator ShootCooldown()
    {
        isOnCooldown = true;

        float timer = cooldownTime;
        while (timer > 0)
        {
            Debug.Log("Cooldown: {Mathf.Ceil(timer)}s remaining...");
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        Debug.Log("Gun is ready to fire again!");
        isOnCooldown = false;
    }
}
