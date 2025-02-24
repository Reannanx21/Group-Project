using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public Vector2 boxSize;
    public float castDistance;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isGrounded()
    {
        
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.DrawWireCube(transform.position - Vector3.up * (castDistance / 2), new Vector3(boxSize.x, boxSize.y, 0));
    }
}