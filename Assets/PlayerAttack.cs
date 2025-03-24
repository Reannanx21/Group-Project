using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public GameObject attackPoint;
    public float radius = 0.5f;
    public LayerMask enemies;
    public float damage = 10f;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is missing on the player!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            anim.SetTrigger("Attack");
            //StartCoroutine(AttackRoutine());
        }
    }

   // private IEnumerator AttackRoutine()
    //{
    //    isAttacking = true;
    //    anim.SetTrigger("Attack");

     //   yield return new WaitForSeconds(0.01f); // Adjust based on animation length

      //  isAttacking = false;
   // }

    public void Attack() // Call this via an Animation Event
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach (Collider2D hit in hitObjects)
        {
            if (hit != null)
            {
                // Check if it's a breakable object
                if (hit.CompareTag("Breakable"))
                {
                    BreakableObject breakable = hit.GetComponent<BreakableObject>();
                    if (breakable != null)
                    {
                        breakable.Break();
                    }
                }
                else // If it's an enemy, apply damage
                {
                    EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        }
    }
}
