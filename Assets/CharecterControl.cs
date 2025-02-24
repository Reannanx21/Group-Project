using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    public float sprintMultiplier = 1.5f;
    public Animator animator;
    private float Move;
    private Rigidbody2D rb;



    [Header("Animation Settings")]
    private Animator anim;

    [Header("Audio Settings")]
    public AudioSource walkingMusic;

    private bool isSprinting;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //if (walkingMusic == null)
        //{
        //    Debug.LogError("Walking music AudioSource is not assigned.");
        // }
    }

    private void Update()
    {
        // Update Move variable with horizontal input
        Move = Input.GetAxisRaw("Horizontal");

        // Check if character is grounded, with a small delay buffer after jumping

        // Update Animator parameters
        anim.SetBool("IsMoving", Move != 0);
        anim.SetBool("IsMovingWalk", Move != 0);
        animator.SetFloat("Speed", Mathf.Abs(Move));

        // Sprinting check: Shift key for sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift) && Move != 0;
        anim.SetBool("Sprinting", isSprinting); // Update Animator parameter for sprinting

        // Adjust speed based on sprinting status
        float adjustedSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.velocity = new Vector2(Move * adjustedSpeed, rb.velocity.y);



    }











}
