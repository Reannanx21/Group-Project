using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    private bool isStunned = false;
    public float stunDuration = 0.5f;

    private CameraShake cameraShake;  // Reference to CameraShake script

    void Start()
    {
        // Find the CameraShake script on the Main Camera automatically
        cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake == null)
        {
            Debug.LogError("CameraShake reference is missing on the Main Camera!");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        // Trigger screen shake when the enemy is hit
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera();  // Shake the camera
        }

        // Trigger stun effect
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine());
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator StunCoroutine()
    {
        isStunned = true;
        // Freeze movement for stun duration (optional: stop animations or modify Rigidbody)
        yield return new WaitForSeconds(stunDuration);  // Wait for the stun duration
        isStunned = false;
    }

    private void Die()
    {
        Destroy(gameObject); // Destroy enemy when health is zero or below
    }
}
