using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.2f;
    private float parryTimer = 0f;
    private bool isParrying = false;
    private Collider2D parryCollider;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;

    private Animator anim;

    void Start()
    {
        parryCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

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

        // Right mouse click = attempt parry
        if (Input.GetMouseButtonDown(1))
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
        if (isParrying) return; // prevents retriggering if already active

        isParrying = true;
        parryTimer = parryWindow;

        if (anim != null)
        {
            anim.ResetTrigger("Parry"); // cleanup just in case
            anim.SetTrigger("Parry");
            anim.SetBool("IsParrying", true); // optional Animator bool
        }

        // Optionally enable collider here if you're using it for hit detection
         parryCollider.enabled = true;
    }

    private void EndParry()
    {
        isParrying = false;
        parryTimer = 0f;

        if (anim != null)
        {
            anim.SetBool("IsParrying", false); // reset Animator bool
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
}
