using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropPlatformW3 : MonoBehaviour
{
    [SerializeField] Rigidbody2D DropObjectRb;
    [SerializeField] float delay=0.5f;
    [SerializeField] float destroyTime= 3f;

    Vector2 originalPosition;

    bool isFalling = false;
    Coroutine fall;

    public void ResetPlatform()
    {
        //StopAllCoroutines();
        StopCoroutine(fall);
        DropObjectRb.bodyType = RigidbodyType2D.Kinematic;
        DropObjectRb.transform.position = originalPosition;
        transform.parent.gameObject.SetActive(true);
        isFalling = false;
    }

    private void Start()
    {
        originalPosition = DropObjectRb.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            fall= StartCoroutine(Fall());
            GameMasterW3.Instance.AddUnsavePlatform(this);
        }
    }

    IEnumerator Fall()
    {
        isFalling = true;
        //yield return new WaitForSeconds(delay);
        for (float timer = 0; timer < delay; timer += Time.deltaTime)
        {
            yield return null;
        }
        DropObjectRb.bodyType = RigidbodyType2D.Dynamic;
        //Destroy(transform.parent.gameObject, destroyTime);
        //yield return new WaitForSeconds(destroyTime);

        for (float timer = 0; timer < destroyTime; timer += Time.deltaTime)
        {
            yield return null;
        }
        transform.parent.gameObject.SetActive(false);
    }

}
