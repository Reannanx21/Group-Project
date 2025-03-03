using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    private float distance;
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Vector2 boxSize;
    public float castDistance;
    public float groundCheckDistance = 1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 20)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            
            RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
            if (groundCheck.collider != null)
            {
                
                rb.MovePosition(new Vector2(rb.position.x + direction.x * speed * Time.deltaTime, rb.position.y + direction.y * speed * Time.deltaTime));
            }
            else
            {
                
                rb.MovePosition(new Vector2(rb.position.x + direction.x * speed * Time.deltaTime, rb.position.y));
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position - Vector3.up * (castDistance / 2), new Vector3(boxSize.x, boxSize.y, 0));
    }
}