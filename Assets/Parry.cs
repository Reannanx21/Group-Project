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
        {
            Debug.LogError("PlayerAttack reference not assigned in ParrySystem!");
        }

        if (anim == null)
        {
            Debug.LogError("Animator component not assigned in ParrySystem!");
        }
    }

    void Update()
    {
        if (playerAttack == null)
        {
            return;
        }

        if (playerAttack.isAttacking || !canParry)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartParry();
        }

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
        isParrying = true;
        parryTimer = parryWindow;

        if (anim != null)
        {
            anim.SetTrigger("Parry");
        }
    }

    private void EndParry()
    {
        isParrying = false;
        parryTimer = 0f;
    }

    private bool canParry
    {
        get
        {
            return !playerAttack.isAttacking && !playerHealth.isDead;
        }
    }
}
