using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth1 : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }
}