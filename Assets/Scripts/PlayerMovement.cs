using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float Move;

    public float speed;

    public float jump;

    public LayerMask groundLayer;

    public Vector2 boxSize;

    public float castDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);

        
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(new Vector2(0, jump * 15), ForceMode2D.Impulse); 
        }
    }

    
    public bool isGrounded()
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

