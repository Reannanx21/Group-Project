using UnityEngine;
using System.Collections;

public class EnemyStun : MonoBehaviour
{
    private bool isStunned = false;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            Debug.Log($"{gameObject.name} is STUNNED for {duration} seconds!");
            isStunned = true;

            // STOP ALL MOVEMENT IMMEDIATELY
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;  // Temporarily disable physics
            }


            StartCoroutine(EndStun(duration));
        }
    }

    private IEnumerator EndStun(float duration)
    {
        yield return new WaitForSeconds(duration);

        Debug.Log($"{gameObject.name} RECOVERED from stun.");
        isStunned = false;

        // Re-enable movement
        if (rb != null)
        {
            rb.isKinematic = false;
        }

    }
}
