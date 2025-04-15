using UnityEngine;
using System.Collections;

public class ObjectHealth : MonoBehaviour
{
    public float health = 50f;
    public GameObject BarrelLight;

    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private bool isBroken = false;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = CameraShake.Instance;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>(); 
    }

    public void TakeDamage(float amount)
    {
        if (isBroken) return;

        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {health}");

        cameraShake.TriggerShake(0.3f, 0.2f);

        if (health <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        if (isBroken) return;
        isBroken = true;

        Debug.Log($"{gameObject.name} is breaking!");

        animator.SetTrigger("isBroken");
        boxCollider.enabled = false;

        if (audioSource != null)
        {
            audioSource.Play(); // Play sound 
        }

        if (BarrelLight != null)
        {
            Destroy(BarrelLight);
        }
    }

    private float GetAnimationLength()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }
}
