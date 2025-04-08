using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float health = 50f;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool IsBroken = false;
    private CameraShake cameraShake;

    private void Start()
    {
        cameraShake = CameraShake.Instance;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Break()
    {
        if (IsBroken) return;
        IsBroken = true;

        Debug.Log($"{gameObject.name} is breaking!");


        boxCollider.enabled = false;


        animator.SetBool("isBroken", true);



        Debug.Log($"{gameObject.name} has died!");
    }

    public void TakeDamage(float amount)
    {
        if (IsBroken) return;

        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage! Remaining health: {health}");

        cameraShake.TriggerShake(0.3f, 0.2f);

        if (health <= 0)
        {
            Break();
        }
    }

}
