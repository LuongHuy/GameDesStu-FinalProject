using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropPlatformW3 : MonoBehaviour
{
    [SerializeField] Rigidbody2D DropObjectRb;
    [SerializeField] float delay=0.5f;
    [SerializeField] float destroyTime= 3f;

    bool isFalling = false;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Debug.Log("Something collide");
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(delay);
        DropObjectRb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(transform.parent.gameObject, destroyTime);
    }

}
