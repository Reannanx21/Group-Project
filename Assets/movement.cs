using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float Move;

    public float speed;
    public float jump;

    public LayerMask groundLayer;

    public Vector2 boxSize;
    public float castDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        if (animator != null)
        {
            animator.SetBool("HenryWalk", Mathf.Abs(Move) > 0.01f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jump * 15), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
    }


    private void CheckGrounded()
    {
        //isGroundedFlag = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - Vector3.up * (castDistance / 2), new Vector3(boxSize.x, boxSize.y, 0));
    }
}