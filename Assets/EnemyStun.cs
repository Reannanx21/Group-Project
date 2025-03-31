using UnityEngine;
using System.Collections;

public class EnemyStun : MonoBehaviour
{
    private bool isStunned = false;
    private Rigidbody2D rb;
    private RigidbodyConstraints2D originalConstraints; // Store original constraints

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            originalConstraints = rb.constraints; // Save the initial constraints
        }
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            Debug.Log($"{gameObject.name} is STUNNED for {duration} seconds!");
            isStunned = true;

            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }

            StartCoroutine(EndStun(duration));
        }
    }

    private IEnumerator EndStun(float duration)
    {
        yield return new WaitForSeconds(duration);

        Debug.Log($"{gameObject.name} RECOVERED from stun.");
        isStunned = false;

        if (rb != null)
        {
            rb.constraints = originalConstraints; 
        }
    }
}
