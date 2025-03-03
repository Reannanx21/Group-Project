using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    private Rigidbody2D rb;

    public float chaseRange = 100f;
    public float sqrChaseRange;

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

        sqrChaseRange = chaseRange * chaseRange;
    }

    void Update()
    {
        if (player == null) return;

        float sqrDistance = (transform.position - player.transform.position).sqrMagnitude;

        if (sqrDistance < sqrChaseRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }
}