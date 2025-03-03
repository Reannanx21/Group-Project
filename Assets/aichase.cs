using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    private float distance;
    private Rigidbody2D rb;

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
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }
    }
}