using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float stopDistance = 3f;
    public LayerMask groundLayer;

    public int maxHealth = 100;
    private int currentHealth;

    private Camera mainCam;
    private Rigidbody2D rb;
    private Animator animator; 

    public float groundDistance = 0.5f;
    public float gravityScale = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);
        float buffer = 0.2f;

        if (distance > stopDistance + buffer)
        {
            MoveEnemy(direction);
        }
        else if (distance < stopDistance - buffer)
        {
            MoveEnemy(-direction);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        
        animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);

        KeepOnGround();
    }

    void MoveEnemy(Vector2 direction)
    {
        Vector2 targetVelocity = direction * moveSpeed;
        rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
    }

    void KeepOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

        if (hit.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, -0.5f);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(CameraShake(0.15f, 0.3f));
    }

    void Die()
    {
        // animator.SetTrigger("isDead"); 
        Destroy(gameObject, 2f);
    }

    System.Collections.IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCam.transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCam.transform.localPosition = originalPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
    }
}
