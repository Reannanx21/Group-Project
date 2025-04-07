using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource footstepAudio; // Add AudioSource

    private float Move;
    private bool isGrounded;

    public float speed;
    public float jump;

    public LayerMask groundLayer;

    public Vector2 boxSize;
    public float castDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>(); // Get the AudioSource

        if (footstepAudio == null)
        {
            Debug.LogError("No AudioSource found on player!");
        }
    }

    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (animator != null)
        {
            animator.SetBool("HenryWalk", Mathf.Abs(Move) > 0.01f);
        }

        isGrounded = IsGrounded();

       

        // Handle footstep sound
        if (Mathf.Abs(Move) > 0.01f && isGrounded)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
                Debug.Log("Playing Footstep Sound");
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Pause();
                Debug.Log("Pausing Footstep Sound");
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jump * 15), ForceMode2D.Impulse);
            footstepAudio.Pause(); // Stop sound when jumping
        }
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
        return grounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - Vector3.up * (castDistance / 2), new Vector3(boxSize.x, boxSize.y, 0));
    }
}