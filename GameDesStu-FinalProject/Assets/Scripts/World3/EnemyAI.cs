using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Boundary")]
    [SerializeField] Collider2D boundaryLeft;
    [SerializeField] Collider2D boundaryRight;

    [Header("Parameter")]
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rd;

    // is the element facing right
    bool facingRight = true;

    public void Act()
    {
        //Debug.Log("Come on, do something");
        rd.velocity = new Vector2( facingRight?1:-1 * speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == boundaryLeft || collision == boundaryRight)
        {
            facingRight = false;
        }
    }
}
