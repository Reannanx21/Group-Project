using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private Animator anim;
    private bool isBroken = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Break()
    {
        if (!isBroken)
        {
            isBroken = true;
            anim.GetBool("isBroken"); // Triggers the animation
           // StartCoroutine(DestroyAfterAnimation());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Break();
        }
    }


  //  private IEnumerator DestroyAfterAnimation()
    //{
      //  float animationTime = anim.GetCurrentAnimatorStateInfo(0).length; // Get animation duration
       // float extraTime = 0.1f; // Additional delay time (in seconds)

       // yield return new WaitForSeconds(animationTime + extraTime); // Wait for animation + extra delay

       // Destroy(gameObject); // Destroy after the delay
    //}


}



