using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.2f;
    private float parryTimer = 0f;
    private bool isParrying = false;
    private float parryCooldown = 0.7f; // cooldown between parries
    private float nextParryTime = 0f;

    private Collider2D parryCollider;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;

    private Animator anim;

    public AudioSource audioSource;
    public AudioClip parrySound;

    void Start()
    {
        parryCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (playerAttack == null)
            Debug.LogError("PlayerAttack reference not assigned in ParrySystem!");

        if (anim == null)
            Debug.LogError("Animator component not assigned in ParrySystem!");
    }

    void Update()
    {
        if (playerAttack == null || playerHealth == null)
            return;

        // If attacking or dead, no parry allowed
        if (playerAttack.isAttacking || !canParry)
            return;

        // Right mouse click = attempt parry, only if cooldown passed
        if (Input.GetMouseButtonDown(1) && Time.time >= nextParryTime)
        {
            StartParry();
        }

        // Count down parry timer
        if (isParrying)
        {
            parryTimer -= Time.deltaTime;
            if (parryTimer <= 0f)
            {
                EndParry();
            }
        }
    }

    private void StartParry()
    {
        audioSource.PlayOneShot(parrySound);
        if (isParrying) return;

        isParrying = true;
        parryTimer = parryWindow;
        nextParryTime = Time.time + parryCooldown;

        if (anim != null)
        {
            anim.ResetTrigger("Parry"); // just to be safe
            anim.SetTrigger("Parry");
            anim.SetBool("IsParrying", true); // optional, use in Animator if needed
        }

        // parryCollider.enabled = true; // optional
    }

    private void EndParry()
    {
        isParrying = false;
        parryTimer = 0f;

        if (anim != null)
        {
            anim.SetBool("IsParrying", false);
        }

        // parryCollider.enabled = false;
    }

    private bool canParry
    {
        get
        {
            return !playerAttack.isAttacking && !playerHealth.isDead;
        }
    }

    // Called from Animation Event
    public void OnParryStart()
    {
        isParrying = true;
    }

    // Called from Animation Event
    public void OnParryEnd()
    {
        EndParry();
    }
}