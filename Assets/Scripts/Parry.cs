using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public float parryWindow = 0.2f;
    private float parryTimer = 0f;
    private bool isParrying = false;
    private float parryCooldown = 0.7f;
    private float nextParryTime = 0f;

    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;

    private Animator anim;

    public AudioSource audioSource;
    public AudioClip parrySound;

    public GameObject parryColliderObject; // NEW: External collider object

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (playerAttack == null)
            Debug.LogError("PlayerAttack reference not assigned in ParrySystem!");

        if (anim == null)
            Debug.LogError("Animator component not assigned in ParrySystem!");

        if (parryColliderObject != null)
            parryColliderObject.SetActive(false); // Ensure it's off at start
    }

    void Update()
    {
        if (playerAttack == null || playerHealth == null)
            return;

        if (playerAttack.isAttacking || !canParry)
            return;

        if (Input.GetMouseButtonDown(1) && Time.time >= nextParryTime)
        {
            Debug.Log("Parry Input Detected");
            StartParry();
        }

        if (isParrying)
        {
            parryTimer -= Time.deltaTime;
            if (parryTimer <= 0f)
            {
                Debug.Log("Parry Timer Ended");
                EndParry();
            }
        }
    }

    private void StartParry()
    {
        if (isParrying) return;

        Debug.Log("Starting Parry!");
        audioSource.PlayOneShot(parrySound);
        isParrying = true;
        parryTimer = parryWindow;
        nextParryTime = Time.time + parryCooldown;

        if (anim != null)
        {
            anim.ResetTrigger("Parry");
            anim.SetTrigger("Parry");
            anim.SetBool("IsParrying", true);
        }

        if (parryColliderObject != null)
        {
            parryColliderObject.SetActive(true); // Enable the parry collider
            Debug.Log("Parry Collider Enabled");
        }
    }

    private void EndParry()
    {
        isParrying = false;
        parryTimer = 0f;

        if (anim != null)
        {
            anim.SetBool("IsParrying", false);
        }

        if (parryColliderObject != null)
        {
            parryColliderObject.SetActive(false); // Disable parry collider
            Debug.Log("Parry Collider Disabled");
        }
    }

    private bool canParry
    {
        get { return !playerAttack.isAttacking && !playerHealth.isDead; }
    }

    public bool IsParrying => isParrying;

    public void OnParryStart()
    {
        isParrying = true;
    }

    public void OnParryEnd()
    {
        EndParry();
    }
}
