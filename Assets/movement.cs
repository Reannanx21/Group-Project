using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource footstepAudio;
    private SpriteRenderer sprite;
    private CameraZoom camZoom;


    private float Move;
    private bool isGrounded;
    private bool isStunned = false;

    public float speed;
    public float jump;

    public LayerMask groundLayer;
    public Vector2 boxSize;
    public float castDistance;

    [Header("Stun FX")]
    public float stunDuration = 2f;
    public GameObject stunVFX; // Optional particle prefab
    public AudioClip stunSFX;

    private AudioSource sfxAudioSource;

    void Start()
    {
        camZoom = Camera.main.GetComponent<CameraZoom>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        sfxAudioSource = GetComponent<AudioSource>();

        if (footstepAudio == null || sprite == null)
        {
            Debug.LogError("Missing required components on Player!");
        }

    }

    void Update()
    {
        if (isStunned)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (animator != null)
        {
            animator.SetBool("HenryWalk", Mathf.Abs(Move) > 0.01f);
        }

        isGrounded = IsGrounded();

        // Footstep sound
        if (Mathf.Abs(Move) > 0.01f && isGrounded)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Pause();
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jump * 15), ForceMode2D.Impulse);
            footstepAudio.Pause();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - Vector3.up * (castDistance / 2), new Vector3(boxSize.x, boxSize.y, 0));
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(HandleStun(duration));
        }
    }

    private IEnumerator HandleStun(float duration)
    {
        isStunned = true;
        rb.velocity = Vector2.zero;

        if (camZoom != null)
            camZoom.ZoomIn(duration);

        if (sprite != null)
            sprite.color = Color.red;

        
        if (stunVFX != null)
        {
            GameObject fx = Instantiate(stunVFX, transform.position, Quaternion.identity, transform);
            Destroy(fx, duration);
        }

        // SFX
        if (stunSFX != null && sfxAudioSource != null)
        {
            sfxAudioSource.PlayOneShot(stunSFX);
        }

        yield return new WaitForSeconds(duration);

        isStunned = false;

        if (sprite != null)
            sprite.color = Color.white;

      

    }
}
